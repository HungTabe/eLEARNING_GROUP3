using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using OnlineLearning.Enums;
using System.Text;
using System.Security.Cryptography;

namespace OnlineLearning.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IUserRoleService _userRoleService;

        public LoginController(IUserService userService, IEmailService emailService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _emailService = emailService;
            _userRoleService = userRoleService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return View();
            }
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["Error"] = "Email không tồn tại!!!";
                return View();
            }

            await _emailService.SendNewPasswordEmailAsync(email);

            TempData["Success"] = "Mật khẩu mới đã được gửi qua email!";
            return View();
        }

        public async Task<IActionResult> Login(string email, string pswd)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData["ErrorLogin"] = " email hoặc mật khẩu không được rỗng!";

                return RedirectToAction("Index");
            }
            var user = await _userService.GetByEmailAndPasswordAsync(email, HashPassword(pswd));

            //Kiểm tra và add claim để phân quyền

            if (user != null)
            {
                // Lấy role của user từ UserRoles
                var roles = user.UserRoles.Select(ur => ur.Role.RoleName.ToString()).ToList(); // neu user co 2 role =>list roles: ['admin','mentee']

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
            };
                // Thêm từng Role vào Claims
                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                //Quyền
                HttpContext.Session.SetString("FullName", user.FullName.ToString());
                HttpContext.Session.SetString("UserId", user.UserId.ToString());

                string avatarUrl = string.IsNullOrEmpty(user.AvatarUrl)
                    ? "/images/default-avatar.jpg" // Nếu null hoặc rỗng, đặt ảnh mặc định
                    : user.AvatarUrl;

                HttpContext.Session.SetString("Avatar", avatarUrl);

                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorLogin"] = "Sai email hoặc mật khẩu!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["activeForm"] = "signup";  // Giữ lại trạng thái Sign Up
                return View("Index");
            }

            var user = await _userService.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                TempData["ErrorLogin"] = "Email đã tồn tại!";
                ViewData["activeForm"] = "signup";
                return View("Index");
            }

            string otp = new Random().Next(100000, 999999).ToString();
            HttpContext.Session.SetString("OTP", otp);
            HttpContext.Session.SetString("UserEmail", model.Email);
            HttpContext.Session.SetString("UserPassword", model.Password);
            HttpContext.Session.SetString("FullName", model.FullName);

            DateTime otpExpiryTime = DateTime.UtcNow.AddMinutes(5);
            HttpContext.Session.SetString("OtpExpiryTime", otpExpiryTime.ToString("o"));

            await _emailService.SendOtpEmailAsync(model.Email, otp);
            return RedirectToAction("VerifyOtp");
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("FullName");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("HeaderPartial");
            return RedirectToAction("Index", "Login");
        }


        public async Task<IActionResult> VerifyOtp(string enteredOtp)
        {
            if (string.IsNullOrEmpty(enteredOtp))
            {
                return View();
            }

            string storedOtp = HttpContext.Session.GetString("OTP");
            string email = HttpContext.Session.GetString("UserEmail");
            string password = HttpContext.Session.GetString("UserPassword");
            string otpExpiryTimeStr = HttpContext.Session.GetString("OtpExpiryTime");
            string fullName = HttpContext.Session.GetString("FullName");


            // Kiểm tra nếu không có OTP hoặc thời gian hết hạn
            if (string.IsNullOrEmpty(storedOtp) || string.IsNullOrEmpty(otpExpiryTimeStr))
            {
                ViewBag.OTPError = "OTP không hợp lệ hoặc đã hết hạn.";
                return View();
            }

            // Chuyển đổi chuỗi thời gian thành DateTime
            DateTime otpExpiryTime = DateTime.Parse(otpExpiryTimeStr, null, System.Globalization.DateTimeStyles.RoundtripKind);

            // Kiểm tra OTP đã hết hạn chưa
            if (DateTime.UtcNow > otpExpiryTime)
            {
                ViewBag.OTPError = "OTP đã hết hạn. Vui lòng yêu cầu lại.";
                return View();
            }

            // Kiểm tra OTP có khớp không
            if (enteredOtp == storedOtp)
            {
                var user = new User
                {
                    Email = email,
                    Password = HashPassword(password),
                    FullName = fullName,
                    CreatedAt = DateTime.UtcNow
                };

                await _userService.AddUserAsync(user);

                //add UserRole
                var createdUser = await _userService.GetUserByEmailAsync(email);

                var userRole = new UserRole
                {
                    UserId = createdUser.UserId,
                    RoleId = 3 // Đặt giá trị RoleId phù hợp
                };

                await _userRoleService.AddUserRoleAsync(userRole);



                // Xóa OTP và thông tin liên quan khỏi Session sau khi xác thực thành công
                HttpContext.Session.Remove("OTP");
                HttpContext.Session.Remove("UserEmail");
                HttpContext.Session.Remove("UserPassword");
                HttpContext.Session.Remove("OtpExpiryTime");
                HttpContext.Session.Remove("FullName");


                TempData["SuccessRegisterMessage"] = "Account Registration Successful!!!";
                TempData.Keep();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.OTPError = "Mã OTP không đúng!";
                return View();
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public async Task<IActionResult> ResendOtp()
        {
            string email = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.OTPError = "Không thể gửi lại OTP. Vui lòng đăng ký lại.";
                return View("VerifyOtp");
            }
            // Tạo OTP mới
            string otp = new Random().Next(100000, 999999).ToString();
            HttpContext.Session.SetString("OTP", otp);

            // Cập nhật thời gian hết hạn mới
            DateTime otpExpiryTime = DateTime.UtcNow.AddMinutes(5);
            HttpContext.Session.SetString("OtpExpiryTime", otpExpiryTime.ToString("o"));

            // Gửi lại OTP qua email
            await _emailService.SendOtpEmailAsync(email, otp);

            ViewBag.ResendOTP = "Mã OTP mới đã được gửi!";
            return View("VerifyOtp");
        }


        public async Task LoginByGoogle()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }

            var googleClaims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Issuer,
                claim.OriginalIssuer,
                claim.Type,
                claim.Value
            });

            var email = googleClaims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (email == null) return RedirectToAction("Index", "Login");

            string emailName = email.Split('@')[0];

            var exitUser = await _userService.GetUserByEmailAsync(email);

            // Nếu user không tồn tại -> tạo mới
            if (exitUser == null)
            {
                var password = "12345678";
                var newUser = new User
                {
                    FullName = emailName,
                    Email = email,
                    Password = HashPassword(password),
                    CreatedAt = DateTime.UtcNow
                };

                var addUser = await _userService.AddUserAsync(newUser);

                var createdUser = await _userService.GetUserByEmailAsync(email);

                var userRole = new UserRole
                {
                    UserId = createdUser.UserId,
                    RoleId = 3 // Đặt giá trị RoleId phù hợp
                };

                await _userRoleService.AddUserRoleAsync(userRole);


                // Đảm bảo user đã được thêm thành công trước khi tiếp tục
                if (addUser == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                exitUser = newUser;
            }






            // Tạo Claims cho user
            var userClaims = new List<Claim>
            {
                    new Claim(ClaimTypes.Email, exitUser.Email)
            };

            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


            var newAddUser = await _userService.GetUserByEmailAsync(email);
            HttpContext.Session.SetString("FullName", newAddUser.FullName.ToString());
            HttpContext.Session.SetString("UserId", newAddUser.UserId.ToString());
            HttpContext.Session.SetString("Avatar", newAddUser.AvatarUrl);

            // Đăng nhập
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }


    }
}

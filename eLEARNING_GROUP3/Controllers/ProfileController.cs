using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Controllers
{
	public class ProfileController : Controller
	{
		private readonly IUserService _userService;
		private readonly IUserRepository _userRepo;

		public ProfileController(IUserService userService, IUserRepository userRepository)
		{
			_userService = userService;
			_userRepo = userRepository;
		}

		public async Task<ActionResult> Index()
		{
			var userIdString = HttpContext.Session.GetString("UserId");

			// Chưa đăng nhập
			if (string.IsNullOrEmpty(userIdString))
			{
				return RedirectToAction("Index", "Login");
			}

			// Lỗi xảy ra 
			if (!long.TryParse(userIdString, out long userId))
			{
				return RedirectToAction("Index", "Login");
			}
			var user = await _userService.GetUserByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}
			// MAP: User to ProfileDTO
			var profileDto = new ProfileDTO
			{
				UserId = user.UserId,
				FullName = user.FullName,
				Phone = user.Phone,
				Email = user.Email,
				AvatarUrl = user.AvatarUrl,
				Gender = user.Gender,
				Dob = user.Dob
			};

			var message = TempData["message"] as String;
			if (!string.IsNullOrEmpty(message) || message != null)
			{
				TempData["message"] = message;
			}

			return View(profileDto);
		}

		// UPDATE PROFILE 
		[HttpPost]
		public async Task<IActionResult> EditProfile(ProfileDTO profile, IFormFile? avatarFile)
		{
			// Check login 
			string userId = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userId))
			{
				return RedirectToAction("Index", "Login");
			}

			// Giữ nguyên email từ db
			var user = await _userService.GetUserByIdAsync(long.Parse(userId));
			if (user == null)
			{
				return NotFound();
			}
			profile.Email = user.Email;
			ModelState.Remove("Email"); // Loại bỏ lỗi validate Email

			// Check valid data
			if (!ModelState.IsValid)
			{
				return View("Index", profile);
			}
			var userByPhone = await _userRepo.GetByPhoneAsync(profile.Phone);

			if (userByPhone != null && !user.Equals(userByPhone)) // Nếu phone đã tồn tại
			{
				ModelState.AddModelError("Phone", "Phone already exists!");
				return View("Index", profile);
			}
			bool isProfileUpdated = await _userService.UpdateProfileAsync(profile);
			bool isAvatarUpdated = true; // Default nếu ko update avt
                                         // Có file ảnh -> cập nhật avatar

          

            if (avatarFile != null && avatarFile.Length > 0)
			{
				isAvatarUpdated = await _userService.ChangeAvatarAsync(profile.UserId, avatarFile);
            }
            var userEdit = await _userService.GetUserByIdAsync(long.Parse(userId));
            HttpContext.Session.SetString("Avatar", userEdit.AvatarUrl);




            // Xử lý thông báo
            TempData["message"] = (isProfileUpdated && isAvatarUpdated) ? "success" : "fail";

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> ChangePassword()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ChangePassword(ChangePassDTO changePassDTO)
		{
			// Check login
			var userIdString = HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userIdString))
			{
				return RedirectToAction("Index", "Login");
			}

			// Check valid input
			if (!ModelState.IsValid)
			{
				return View("ChangePassword");
			}

			// Not found user info
			var userId = long.Parse(userIdString);
			var user = await _userService.GetUserByIdAsync(userId);
			if (user == null)
			{
				return NotFound();
			}

			// Check old pass
			if (!BCrypt.Net.BCrypt.Verify(changePassDTO.CurrentPassword, user.Password))
			{
				ModelState.AddModelError(nameof(changePassDTO.CurrentPassword), "Incorrect current password");
				return View("ChangePassword");
			}

			// Check cannot enter new pass same old pass
			if (BCrypt.Net.BCrypt.Verify(changePassDTO.NewPassword, user.Password))
			{
				ModelState.AddModelError(nameof(changePassDTO.NewPassword), "New password cannot be the same with current password");
				return View("ChangePassword");
			}

			// Change pass
			bool changeResult = await _userService.ChangePasswordAsync(userId, changePassDTO);
			if (!changeResult)
			{
				TempData["message"] = "update_failed";
				return View("ChangePassword");
			}
			TempData["message"] = "success";
			return RedirectToAction(nameof(ChangePassword));
		}
	}
}

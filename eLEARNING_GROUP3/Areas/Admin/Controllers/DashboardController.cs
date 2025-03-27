using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Implementations.Admin;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Areas.Admin.Controllers
{

    [Authorize(Roles = nameof(RoleType.ADMIN))]
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly IUserManagementService _userManagement;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public DashboardController(IUserManagementService userManagement, IUserService userService, INotificationService notificationService) {
            _userManagement = userManagement;
            _userService = userService;
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new {area = ""});// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}
            /////////////////-> Ko cần kiểm tra đăng nhạp. Ko có quyền thì cho vào access deny luôn

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            return View();

        }

        public async Task<IActionResult> AddUser ()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            }

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            ViewBag.Roles = await _userManagement.GetAllRole();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Admin_AddUserDto model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new {area = ""});// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}
            /////////////////-> Ko cần kiểm tra đăng nhạp. Ko có quyền thì cho vào access deny luôn

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // Hoặc log ra file
                }
                ViewBag.Roles = await _userManagement.GetAllRole();

                return View(model);
            }
            try
            {
               await _userManagement.AddUser(model);
                TempData["success"] = "Add new user successfully";
                return RedirectToAction("ListUser");
            }
            catch (Exception ex) {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                switch (ex.Message) {
                    case "Email already exists":
                        ViewData["EmailExist"] = "Email already exists";
                        break;
                    case "Phone already exists":
                        ViewData["PhoneExist"] = "Phone already exists";
                        break;
            }
                
                
                ViewBag.Roles = await _userManagement.GetAllRole();
                return View(model); // Trả về view nếu có lỗi
            }

        }

        [HttpGet]

        public async Task<IActionResult> EditUserAsync(long id)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            }

            var userTmp = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = userTmp;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            var user = await _userManagement.GetUserAndRolesAsync(id);
            if (user == null)
            {
                TempData["notfounduser"] = "true";
                return RedirectToAction("ListUser", "Admin");
            }
            ViewBag.Roles = await _userManagement.GetAllRole();
            var userDto = new Admin_AddUserDto
            {
                Id = user.UserId,
                Fullname = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Gender = user.Gender,
                Dob = user.Dob ?? DateOnly.FromDateTime(DateTime.Today),
                RoleId = user.UserRoles.FirstOrDefault()?.RoleId ?? 0,
                Avatar_str = user.AvatarUrl,
                Password = "",
            };

            return View("EditUser", userDto);
        }


        [HttpPost]
        public async Task<IActionResult> EditUserAsync(Admin_AddUserDto model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new {area = ""});// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}
            /////////////////-> Ko cần kiểm tra đăng nhạp. Ko có quyền thì cho vào access deny luôn

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();

            // Bỏ qua lỗi [Required] của Password và RePassword nếu để trống
            if (string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.RePassword))
            {
                ModelState.Remove("Password");
                ModelState.Remove("RePassword");
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _userManagement.GetAllRole();
                return View(model);
            }

            try
            {
               await _userManagement.UpdateUser(model);
                TempData["success"] = "User updated successfully";
                return RedirectToAction("ListUser");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error: {ex.Message}");
                ViewBag.Roles = await _userManagement.GetAllRole();
                switch(ex.Message)
                {
                    case "user Not Found":
                        ViewData["userNotFound"] = "User Not Found";
                        break;
                }

                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserAsync(long id)
        {
            try
            {
               await _userManagement.DeleteUser(id);
                TempData["success"] = "User deleted successfully";
                return RedirectToAction("ListUser");
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "user Not Found":
                        ViewData["userNotFound"] = "User Not Found";
                        break;
                }
                return View();
            }
            
        }
        public async Task<IActionResult> ListUser ()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            }

            var userTmp = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = userTmp;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            var user = await _userManagement.GetAllUser();
            return View(user);
        }


        public async Task<IActionResult> NotificationAsync()
        {
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            return View();
        }

        public async Task<IActionResult> payWithMomo()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            // Chưa đăng nhập
            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            }

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            return View("TestPayMomo");
        }


    }
}

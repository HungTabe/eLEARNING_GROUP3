using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Enums;
using OnlineLearning.Hubs;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(RoleType.ADMIN))]
    [Area("Admin")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReviewCourseController : Controller
    {
        private readonly ICourseService _courseService;
        protected readonly OnlLearnDBContext _context;
        private readonly INotificationService _notificationService;
        private readonly IUserService _userService;
        private readonly Services.Interfaces.ILanguageService _languageService;
        private readonly ILevelService _levelService;
        private readonly ICategoryService _categoryService;
        private readonly ICourseImageService _courseImageService;
        private readonly ICourseCategoriesService _courseCategoriesService;
        private readonly IHubContext<CourseNotificationHub> _hubContext;

        public ReviewCourseController(IHubContext<CourseNotificationHub> hubContext, IUserService userService, ICourseService courseService, OnlLearnDBContext context, INotificationService notificationService, Services.Interfaces.ILanguageService languageService, ILevelService levelService, ICategoryService categoryService, ICourseImageService courseImageService, ICourseCategoriesService courseCategoriesService)
        {
            _context = context;
            _courseService = courseService;
            _notificationService = notificationService;
            _languageService = languageService;
            _levelService = levelService;
            _categoryService = categoryService;
            _courseImageService = courseImageService;
            _courseCategoriesService = courseCategoriesService;
            _userService = userService;
            _hubContext = hubContext;
        }
        


        public async Task<IActionResult> Index()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            var listCourse = await _courseService.GetCoursesListNotApproveYetAsync();
            return View("ListReviewCourse", listCourse);
        }


        [Route("Admin/ReviewCourse/Detail/{courseId}")]
        //[HttpGet("{courseId}")]
        public async Task<IActionResult> DetailAsync(long courseId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            //// Chưa đăng nhập
            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}

            var user = await _userService.GetUserByIdAsync(long.Parse(userIdString));

            ViewBag.User = user;
            //set notification is readed
            await _notificationService.IsReaded(courseId);
            ViewBag.NotificationsCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;
            ViewBag.Notifications = await _notificationService.GetAllNotifications() ?? new List<Notification>();
            if (courseId != 0)
            {


                var course = await _courseService.GetCourseByIdAsync(courseId);

                if (course == null)
                {
                    return NotFound();
                }

                var selectedCategoryId = await _courseCategoriesService.GetCategoryIdsByCourseIdAsync(courseId);
                var listExistingCourseImage = await _courseImageService.GetListImgByCourseIdAsync(courseId);
                var creator = await _userService.GetUserByIdAsync(course.Creator);
                var adminId = HttpContext.Session.GetString("UserId"); // Lấy AdminId từ Session

                var courseDto = new Admin_ReviewCourseDto
                {
                    CourseId = course.CourseId,
                    CourseName = course.CourseName,
                    CourseUrls = listExistingCourseImage,
                    Description = course.Description,
                    Price = course.Price,
                    Discount = course.Discount,
                    LanguageId = course.LanguageId,
                    LevelId = course.LevelId,
                    CategoryId = selectedCategoryId,
                    CreatorName = creator?.FullName,
                    AdminId = Int32.Parse(adminId) //để tạm
                };

                var courseLevels = await _levelService.GetAllLevelAysnc();
                var courseLanguage = await _languageService.GetAllLanguageAysnc();

                var allCategories = await _categoryService.GetAllCategoryAysnc();
                ViewBag.CourseLevels = new SelectList(courseLevels, "LevelId", "LevelName", course.LevelId);
                ViewBag.CourseLanguages = new SelectList(courseLanguage, "LanguageId", "LanguageName", course.LanguageId);

                ViewData["CourseCategories"] = new SelectList(allCategories, "CategoryId", "CategoryName", selectedCategoryId);
              
                // Gửi tín hiệu giảm số lượng thông báo thông qua SignalR

                //await _hubContext.Clients.All.SendAsync("UpdateNotificationCount", (await _notificationService.GetAllNotificationsIsNotRead()).Count);
                return View("DetailReviewCourse", courseDto);
            }
            else
            {
                TempData["errorMsg"] = "Course does not exist";
                return RedirectToAction("Index");
            }
            
        }

        [HttpPost]
        
        public async Task<IActionResult> ApproveCourseAsync(Admin_ReviewCourseDto model)
        {
            if (ModelState.IsValid) { 
                return View(model);
            }
            var course = await _context.Courses.FindAsync(model.CourseId);
            if (course == null) return NotFound();
            course.Status = Enums.CourseStatus.Approved; // Giả sử có cột Status

            var adminReviewCourse = new AdminReviewCourse
            {
                CourseId = model.CourseId,
                AdminId = model.AdminId,
                Status = Enums.ReviewStatus.Approved,
                ReviewNotes = model.ReviewNote ?? "",
                ReviewedAt = DateTime.UtcNow,
            };
            await _context.AdminReviewCourses.AddAsync(adminReviewCourse);

            await _context.SaveChangesAsync();
            TempData["Message"] = "You are Approve the course successfully!";
            return RedirectToAction("Index");
        }
        [HttpPost]
        
        public async Task<IActionResult> RejectCourseAsync(Admin_ReviewCourseDto model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            var course = await _context.Courses.FindAsync(model.CourseId);
            if (course == null) return NotFound();
            course.Status = Enums.CourseStatus.Rejected;

            var adminReviewCourse = new AdminReviewCourse
            {
                CourseId = model.CourseId,
                AdminId = model.AdminId,
                Status = Enums.ReviewStatus.Rejected,
                ReviewNotes = model.ReviewNote,
                ReviewedAt = DateTime.UtcNow,
            };
            await _context.AdminReviewCourses.AddAsync(adminReviewCourse);

            await _context.SaveChangesAsync();
            TempData["Message"] = "You are Reject the course successfully!";
            return RedirectToAction("Index");

        }
    }
}

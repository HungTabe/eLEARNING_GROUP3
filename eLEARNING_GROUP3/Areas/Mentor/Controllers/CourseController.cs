using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Enums;
using OnlineLearning.Hubs;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Areas.Mentor.Controllers
{
    [Authorize(Roles = nameof(RoleType.MENTOR))]
    [Area("Mentor")]
    public class CourseController : Controller
	{
		private readonly Services.Interfaces.ILanguageService _languageService;
		private readonly ILevelService _levelService;
		private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly ICourseImageService _courseImageService;
        private readonly ICourseCategoriesService _courseCategoriesService;
        private readonly IModuleService _moduleService;
        private readonly INotificationService _notify;

        private readonly IHubContext<CourseNotificationHub> _hubContext;

        public CourseController(Services.Interfaces.ILanguageService languageService, ILevelService levelService, ICourseService courseService, ICategoryService categoryService, ICourseImageService courseImageService, ICourseCategoriesService courseCategoriesService, IModuleService moduleService, IHubContext<CourseNotificationHub> hubContext, INotificationService notificationService)
        {
            _languageService = languageService;
            _levelService = levelService;
            _courseService = courseService;
            _categoryService = categoryService;
            _courseImageService = courseImageService;
            _courseCategoriesService = courseCategoriesService;
            _moduleService = moduleService;
            _hubContext = hubContext;
            _notify = notificationService;
        }


        public async Task<IActionResult> Index()
		{
            //var userIdString = HttpContext.Session.GetString("UserId");

            //if (string.IsNullOrEmpty(userIdString))
            //{
            //    return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            //}
            /////////////////-> Ko cần kiểm tra đăng nhạp. Ko có quyền thì cho vào access deny luôn

            var listAllCourse = await _courseService.GetAllCourseAsync();
			return View(listAllCourse.ToList());
		}




		public async Task<IActionResult> AddCourse()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToAction("Index", "Login", new { area = "" });// Chỉ định area = null để chuyển hướng ra khỏi area hiện tại
            }

            var courseLevels = await _levelService.GetAllLevelAysnc();
			var courseLanguage = await _languageService.GetAllLanguageAysnc();
            var courseCategories = await _categoryService.GetAllCategoryAysnc();



            ViewBag.CourseLevels = new SelectList(courseLevels, "LevelId", "LevelName");
            ViewBag.CourseLanguages = new SelectList(courseLanguage, "LanguageId", "LanguageName");
            ViewBag.CourseCategories = new SelectList(courseCategories, "CategoryId", "CategoryName");

            return View(new CourseDto());
		}

       


        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseDto model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long.TryParse(userIdString, out long userId); // Chuyển đổi UserId từ string -> long


            if (model.CourseName.Length > 255)
            {
                ModelState.AddModelError("CourseName", "CourseName không được vượt quá 255 ký tự!");
            }

            if (!ModelState.IsValid)
            {
                var courseLevels = await _levelService.GetAllLevelAysnc();
                var courseLanguages = await _languageService.GetAllLanguageAysnc();
                var courseCategories = await _categoryService.GetAllCategoryAysnc();

                ViewBag.CourseLevels = new SelectList(courseLevels, "LevelId", "LevelName");
                ViewBag.CourseLanguages = new SelectList(courseLanguages, "LanguageId", "LanguageName");
                ViewBag.CourseCategories = new SelectList(courseCategories, "CategoryId", "CategoryName");

                return View(model);
            }

            // Lưu dữ liệu vào DB (giả sử có service xử lý)

            var newCourse = new Course
            {
                CourseName = model.CourseName,
                Description = model.Description,
                Price = model.Price,
                Discount = model.Discount,
                Creator = userId, 
                Acceptor = null,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                PublishedAt = null,
                DeletedAt = null,
                StudyTime = null,
                LanguageId = model.LanguageId,
                LevelId = model.LevelId,
                Status = CourseStatus.Pending,
            };

            await _courseService.AddCourseAsync(newCourse);


          
                var newCourseCategory = new CourseCategory
                {
                    CourseId = newCourse.CourseId,
                    CategoryId = model.CategoryId,
                    CreatedAt = DateTime.Now
                };

                await _courseCategoriesService.AddCourseCategoryAsync(newCourseCategory);
            

            
                var newCourseImage = new CourseImageUrl
                {
                    CourseId = newCourse.CourseId,
                    Url = model.CourseUrl
                };

                await _courseImageService.AddCourseImageUrlAsync(newCourseImage);

            // Thêm thông báo
            var notification = new Notification
            {
                CourseId = newCourse.CourseId,
                CourseName = newCourse.CourseName,
                CourseUrl = newCourseImage.Url,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            await _notify.AddNotification(notification);
            // Gửi thông báo đến admin qua SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveCourseNotification",
                new {
                    CourseId = newCourse.CourseId,
                    CourseName = newCourse.CourseName,
                    CourseUrl = newCourseImage.Url,
                    CreatedAt = newCourse.CreatedAt
                });

            TempData["Success"] = "Khóa học mới đã được thêm vào!";

            return RedirectToAction("Index"); 
        }


        [HttpGet("Course/HiddenCourse/{courseId}")]
        public async Task<IActionResult> HiddenCourse(long courseId)
        {
            var hiddenCourse = await _courseService.GetCourseByIdAsync(courseId);
            if (hiddenCourse == null) {
                return NotFound();
            }

            hiddenCourse.Status = CourseStatus.Draft;
            await _courseService.UpdateCourseAsync(hiddenCourse);

            TempData["Success"] = "Cập nhật thành công!";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> CourseEdit(long id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }


            var selectedCategoryId = await _courseCategoriesService.GetCategoryIdsByCourseIdAsync(id);
            var existingCourseImage = await _courseImageService.GetByCourseIdAsync(id);

            var courseDto = new CourseEditDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseUrl = existingCourseImage.Url, 
                Description = course.Description,
                Price = course.Price,
                Discount = course.Discount,
                LanguageId = course.LanguageId,
                LevelId = course.LevelId,
                OldCategoryId = selectedCategoryId,
            };

            var courseLevels = await _levelService.GetAllLevelAysnc();
            var courseLanguage = await _languageService.GetAllLanguageAysnc();

            var allCategories = await _categoryService.GetAllCategoryAysnc();

            var allModuleByCourseId = await _moduleService.GetAllModuleByCourseId(id);
            ViewBag.Modules = allModuleByCourseId;

            ViewBag.CourseLevels = new SelectList(courseLevels, "LevelId", "LevelName", course.LevelId);
            ViewBag.CourseLanguages = new SelectList(courseLanguage, "LanguageId", "LanguageName", course.LanguageId);
          

            ViewData["CourseCategories"] = new SelectList(allCategories, "CategoryId", "CategoryName", selectedCategoryId);



            return View("editCourse", courseDto); // Hiển thị View với thông tin khóa học
        }


        [HttpPost]

        public async Task<IActionResult> EditCourse(CourseEditDto model)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            long.TryParse(userIdString, out long userId);

            if (model.CourseName.Length > 255)
            {
                ModelState.AddModelError("CourseName", "CourseName không được vượt quá 255 ký tự!");
            }
            if (!ModelState.IsValid)
            {
                var courseLevels = await _levelService.GetAllLevelAysnc();
                var courseLanguages = await _languageService.GetAllLanguageAysnc();
                var courseCategories = await _categoryService.GetAllCategoryAysnc();

                ViewBag.CourseLevels = new SelectList(courseLevels, "LevelId", "LevelName");
                ViewBag.CourseLanguages = new SelectList(courseLanguages, "LanguageId", "LanguageName");
                ViewBag.CourseCategories = new SelectList(courseCategories, "CategoryId", "CategoryName");

                return View(model);
            }


            var existingCourse = await _courseService.GetCourseByIdAsync(model.CourseId);
            if (existingCourse == null)
            {
                return NotFound();
            }
            existingCourse.CourseName = model.CourseName;
            existingCourse.Description = model.Description;
            existingCourse.Price = model.Price;
            existingCourse.Discount = model.Discount;
            existingCourse.LanguageId = model.LanguageId;
            existingCourse.LevelId = model.LevelId;
            existingCourse.Status = CourseStatus.Pending;
            existingCourse.UpdatedAt = DateTime.Now;

            await _courseService.UpdateCourseAsync(existingCourse);

            var existingCourseCategory = await _courseCategoriesService.GetByCourseIdAsync(model.CourseId,model.OldCategoryId.Value);
            if (existingCourseCategory != null)
            {
                await _courseCategoriesService.DeleteCourseCategoryAsync(existingCourseCategory);
            }
            var newCourseCategory = new CourseCategory
            {
                CourseId = model.CourseId,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now
            };
            await _courseCategoriesService.AddCourseCategoryAsync(newCourseCategory);


            var existingCourseImage = await _courseImageService.GetByCourseIdAsync(model.CourseId);

            if (existingCourseImage == null)
            {
                return NotFound();
            }

            existingCourseImage.Url = model.CourseUrl;
            await _courseImageService.UpdateCourseImageUrAsync(existingCourseImage);

            TempData["Success"] = "Update học mới thành công!";

            return RedirectToAction("Index");

        }



    }
}

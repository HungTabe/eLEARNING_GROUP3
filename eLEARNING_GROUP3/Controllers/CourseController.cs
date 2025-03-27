using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }



        public async Task<IActionResult> Index()
        {
            var listCourse = await _courseService.GetAllCourseByStatusAsync();

            return View(listCourse);
        }


        [HttpGet("Course/DetailsCourse/{courseId}")]

        public async Task<IActionResult> DetailsCourse(long courseId)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            var checkPurchase = false;
            if (!string.IsNullOrEmpty(userIdString))
            {
                var userId = long.Parse(userIdString);
                checkPurchase = await _courseService.CheckPurchaseCourseAsync(userId, courseId);
            }

            var course = await _courseService.GetCourseDetailAsync(courseId);
            if (course == null)
            {
                return NotFound();
            }

            ViewBag.checkPurchase = checkPurchase;
            return View(course);
        }

    }
}

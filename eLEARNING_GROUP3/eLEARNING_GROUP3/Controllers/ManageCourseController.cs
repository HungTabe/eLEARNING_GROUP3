using Microsoft.AspNetCore.Mvc;

namespace eLEARNING_GROUP3.Controllers
{
    public class ManageCourseController
    {
        _courseCategoriesService = courseCategoriesService;
        }

    public IActionResult Index()
        public async Task<IActionResult> Index()
    {
        return View();
        var listAllCourse = await _courseService.GetAllCourseAsync();
        return View(listAllCourse.ToList());
    }




    public async Task<IActionResult> AddCourse()
    {
        var userIdString = HttpContext.Session.GetString("UserId");
    }
}

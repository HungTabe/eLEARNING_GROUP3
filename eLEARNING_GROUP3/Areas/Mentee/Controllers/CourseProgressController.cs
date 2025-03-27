using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Interfaces;
using System.Collections.Generic;

namespace OnlineLearning.Areas.Mentee.Controllers
{
    [Area("Mentee")]
    public class CourseProgressController : Controller
    {
        private readonly IMyCourseService _myCourseService;
        public CourseProgressController(IMyCourseService myCourseService) { _myCourseService = myCourseService; }
        public async Task<IActionResult> Index()
        {
            try
            {
                long userId = long.Parse(HttpContext.Session.GetString("UserId"));
                List<MyCourseDto> listCourses = await _myCourseService.GetMyCourseList(userId);
                List<WishlistDTO> wishLists = await _myCourseService.GetMyWishlist(userId);
                ViewBag.WishLists = wishLists;
                return View("CourseProgress",listCourses);
                
            }
            catch (Exception ex) { }


            return View("CourseProgress");
        }


        //[HttpGet("/Mentee/")]
        //public async Task<IActionResult> MyWishListAsync(long userId)
        //{
        //    return View();
        //}





        [HttpPost]
        public async Task<IActionResult> AddToWishlist(long courseId)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdStr))
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            if (!long.TryParse(userIdStr, out long userId))
            {
                return Json(new { success = false, message = "Invalid user ID." });
            }

            var result = await _myCourseService.AddToWishlistAsync(userId, courseId);
            return Json(result);
        }

    }
}

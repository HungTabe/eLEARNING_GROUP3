using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.Order;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Areas.Mentee.Controllers
{
	[Area("Mentee")]
	public class LessonController : Controller
	{
		private readonly ICourseService _courseService;
		public LessonController(ICourseService courseService)
		{
			_courseService = courseService;
		}
		[HttpGet("Mentee/Lesson/{courseId}/{lessonId?}")]
		public async Task<ActionResult> IndexAsync(long courseId, long? lessonId)
		{
			// check đăng nhập
			if (!HttpContext.Session.TryGetValue("UserId", out var userIdBytes) ||
				!long.TryParse(System.Text.Encoding.UTF8.GetString(userIdBytes), out var userId))
			{
				return RedirectToAction("Index", "Login", new { area = "" });
			}

			

			// Kiểm tra quyền truy cập khóa học
			if (!await _courseService.CheckPurchaseCourseAsync(userId, courseId))
			{
				//GET COURSE INFORMATION
				var courseInfor = await _courseService.GetCourseByIdAsync(courseId);
				string? fullName = HttpContext.Session.GetString("UserId");

                // CHECKOUT
                return RedirectToAction("CreatePaymentMomo", "Payment", new {
					Area = "",
                    amount = courseInfor.Price,
                    orderInfor = $"Thanh toán khóa học {courseInfor.CourseName}",
                    paymentUserId = userId,
                    paymentCourseId = courseId,
					paymentFullName = fullName
                });
			}

			var course = await _courseService.GetCourseDetailAsync(courseId);

			// Không có lessonId => default: Id first lesson
			if (!lessonId.HasValue)
			{
				lessonId = course.Modules
		   .OrderBy(m => m.ModuleNumber) // Sắp xếp Module theo số thứ tự
		   .SelectMany(m => m.Lessons)   // Lấy tất cả bài học trong Modules
		   .OrderBy(l => l.LessonNumber) // Sắp xếp bài học theo số thứ tự
		   .FirstOrDefault()?.LessonId;
			}

			ViewBag.LessonId = lessonId;
			ViewBag.ModuleId = course.Modules
									.FirstOrDefault(m => m.Lessons.Any(l => l.LessonId == lessonId))?
									.ModuleId;
			return View(course);
		}
	}
}

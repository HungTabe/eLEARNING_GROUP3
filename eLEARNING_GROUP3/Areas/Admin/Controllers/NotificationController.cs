using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Implementations.Admin;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Areas.Admin.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task<IActionResult> Index()
        {
            //ViewBag.Notifications = await _notificationService.GetNotifications() ?? new List<Notification>();
            return View();
        }
    }
}

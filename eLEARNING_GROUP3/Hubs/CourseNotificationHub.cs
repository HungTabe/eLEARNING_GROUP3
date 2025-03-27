using Microsoft.AspNetCore.SignalR;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Hubs
{
    public class CourseNotificationHub : Hub
    {
        private readonly INotificationService _notificationService;
        public CourseNotificationHub(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        // Gửi thông báo mới khi có khóa học mới
        public async Task SendNotification(Notification notification)
        {
            await Clients.All.SendAsync("ReceiveCourseNotification", notification);
        }

        // Đánh dấu thông báo là đã đọc
        //public async Task MarkNotificationAsRead(long courseId)
        //{
        //    await _notificationService.IsReaded(courseId);
        //    var newCount = (await _notificationService.GetAllNotificationsIsNotRead()).Count;

        //    await Clients.Caller.SendAsync("UpdateNotificationCount", newCount);
        //}
    }
}

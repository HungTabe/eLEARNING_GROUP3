using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;

namespace OnlineLearning.Services.Implementations.Admin
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepo;
        public NotificationService(INotificationRepository notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }
        public async Task AddNotification(Notification notification)
        {
            await _notificationRepo.AddAsync(notification);
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _notificationRepo.GetAllNotificationAysnc();
        } 
        public async Task<List<Notification>> GetAllNotificationsIsNotRead()
        {
            return await _notificationRepo.GetNotificationIsNotRead();
        }

        public async Task IsReaded(long courseId)
        {
            var notification = await _notificationRepo.GetNotificationByCourseId(courseId);
            if (notification == null) { throw new Exception("NotFound"); }

            notification.IsRead = true;
            await _notificationRepo.UpdateAsync(notification);
        }
    }
}

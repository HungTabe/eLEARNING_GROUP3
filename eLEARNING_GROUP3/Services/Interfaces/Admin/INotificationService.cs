using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Services.Interfaces.Admin
{
    public interface INotificationService
    {
        Task AddNotification(Notification notification);
        Task IsReaded(long courseId);
        Task<List<Notification>> GetAllNotifications();

        Task<List<Notification>> GetAllNotificationsIsNotRead();
    }
}

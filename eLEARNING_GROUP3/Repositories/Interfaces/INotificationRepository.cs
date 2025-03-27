using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        Task<List<Notification>> GetNotificationIsNotRead();
        Task<List<Notification>> GetAllNotificationAysnc();
        Task<Notification> GetNotificationByCourseId(long courseId);
    }
}

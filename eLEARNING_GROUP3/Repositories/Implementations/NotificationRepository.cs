using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(OnlLearnDBContext context) : base(context) { }

        public async Task<List<Notification>> GetAllNotificationAysnc()
        {
            return await _context.Notifications.ToListAsync();

        }

        public async Task<Notification> GetNotificationByCourseId(long courseId)
        {
            
            return await _context.Notifications.FirstOrDefaultAsync(x => x.CourseId == courseId);
       
        }

        public async Task<List<Notification>> GetNotificationIsNotRead()
        {
            // Lấy danh sách thông báo chưa đọc
            var notifications = await _context.Notifications
                .Where(n => n.IsRead == false)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();
            return notifications;
        }
    }
}

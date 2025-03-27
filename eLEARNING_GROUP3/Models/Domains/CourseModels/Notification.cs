using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.CourseModels
{
    public class Notification
    {
        public long NotificationId { get; set; }
        public long CourseId { get; set; }
        [Required]
        public string CourseName { get; set; } = null!;
        [Required]
        public string CourseUrl { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; } = false;

        //public string NotificationType { get; set; } = "";
        public virtual Course Course { get; set; } = null!;
    }
}

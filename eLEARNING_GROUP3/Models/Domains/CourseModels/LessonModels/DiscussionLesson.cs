using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.CourseModels.LessonModels
{
    public class DiscussionLesson
    {
        [Key]
        public long DisscussionId { get; set; }

        public long? ParentCommentID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        public string Comment { get; set; } = string.Empty;
        public long? UserId { get; set; }
        public long? LessonId { get; set; }

        public virtual DiscussionLesson? ParentComment { get; set; }
        public virtual User? User { get; set; }
        public virtual Lesson? Lesson { get; set; }
    }
}
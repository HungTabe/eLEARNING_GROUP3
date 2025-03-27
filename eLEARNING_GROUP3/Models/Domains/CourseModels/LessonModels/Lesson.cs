using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Models.Domains.CourseModels.LessonModels
{
    public class Lesson
    {

        [Key]
        public long LessonId { get; set; }

        public long ModuleId { get; set; }

        [Required]
        public int LessonNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string LessonName { get; set; } = null!;

        public string? LessonContent { get; set; }

        public string? LessonVideo { get; set; }

        public int? Duration { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        public virtual Module Module { get; set; } = null!;
        public virtual ICollection<DiscussionLesson> Discussions { get; set; } = new List<DiscussionLesson>();
    }
}
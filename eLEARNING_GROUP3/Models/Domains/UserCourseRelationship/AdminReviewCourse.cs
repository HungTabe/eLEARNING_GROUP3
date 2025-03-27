using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class AdminReviewCourse
    {
        [Key]
        public long ReviewId { get; set; }

        public long CourseId { get; set; }

        public long AdminId { get; set; }

        [Required]
        public ReviewStatus Status { get; set; }

        public string? ReviewNotes { get; set; }

        [Required]
        public DateTime ReviewedAt { get; set; } = DateTime.Now;

        public virtual Course Course { get; set; } = null!;
        public virtual User Admin { get; set; } = null!;
    }
}

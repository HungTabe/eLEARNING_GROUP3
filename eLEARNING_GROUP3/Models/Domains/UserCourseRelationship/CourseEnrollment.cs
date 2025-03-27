using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class CourseEnrollment
    {
        public long CourseId { get; set; }
        public long UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public EnrollmentStatus Status { get; set; }

        [Required]
        public int Progress { get; set; } = 0;

        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

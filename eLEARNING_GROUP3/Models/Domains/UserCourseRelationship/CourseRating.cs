using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class CourseRating
    {
        [Key]
        public long RatingId { get; set; }

        [Required]
        public byte Rating { get; set; }

        public string? Feedback { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public long CourseId { get; set; }

        public long UserId { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Models.Domains.UserModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class UserAnswer
    {
        [Key]
        public long UserAnswerId { get; set; }
        public long UserId { get; set; }
        public long QuestionId { get; set; }

        public long? OptionId { get; set; }

        [MaxLength(255)]
        public string? AnswerText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [MaxLength(20)]
        public CommonStatus Status { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Question Question { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class UserQuizResult
    {
        [Key]
        public long UserQuizResultId { get; set; }
        public long UserId { get; set; }
        public long QuizId { get; set; }

        [Column(TypeName = "decimal(5,2)"), Required]
        public decimal Score { get; set; }
        [Required]
        public int TotalQuestions { get; set; }
        [Required]
        public int CorrectAnswers { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; } = null!;
        public virtual Quiz Quiz { get; set; } = null!;
    }
}

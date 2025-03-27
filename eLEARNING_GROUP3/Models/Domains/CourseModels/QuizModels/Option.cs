using OnlineLearning.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.CourseModels.QuizModels
{
    public class Option
    {
        [Key]
        public long OptionId { get; set; }

        public long QuestionId { get; set; }

        [Required]
        [StringLength(255)]
        public string OptionText { get; set; } = null!;

        [Required]
        public bool IsCorrect { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        public virtual Question Question { get; set; } = null!;
    }
}
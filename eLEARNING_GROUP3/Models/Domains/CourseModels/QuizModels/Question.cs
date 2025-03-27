using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;

namespace OnlineLearning.Models.Domains.CourseModels.QuizModels
{
    public class Question
    {
        [Key]
        public long QuestionId { get; set; }

        [Required]
        public int QuestionNum { get; set; }

        public long QuizId { get; set; }

        [Required]
        [StringLength(255)]
        public string QuestionName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string Type { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        public virtual Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
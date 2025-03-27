using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;

namespace OnlineLearning.Models.Domains.CourseModels
{
    public class Module
    {
        [Key]
        public long ModuleId { get; set; }

        [Required]
        [StringLength(255)]
        public string ModuleName { get; set; } = null!;

        public long CourseId { get; set; }

        [Required]
        public int ModuleNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public CommonStatus Status { get; set; }

        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}

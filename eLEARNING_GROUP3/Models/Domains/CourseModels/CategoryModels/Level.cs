using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Models.Domains.CourseModels.CategoryModels
{
    public class Level
    {
        [Key]
        public long LevelId { get; set; }

        [StringLength(255)]
        public string? LevelName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public CategoryStatus Status { get; set; }

        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}

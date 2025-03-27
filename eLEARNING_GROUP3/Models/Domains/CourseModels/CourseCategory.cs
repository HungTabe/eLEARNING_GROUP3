using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Models.Domains.CourseModels
{
    public class CourseCategory
    {
        public long CategoryId { get; set; }
        public long CourseId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Category Category { get; set; } = null!;
        public virtual Course Course { get; set; } = null!;
    }
}
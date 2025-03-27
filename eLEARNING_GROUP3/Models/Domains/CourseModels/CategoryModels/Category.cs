using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Models.Domains.CourseModels.CategoryModels
{
    public class Category
    {
        [Key]
        public long CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string CategoryName { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public CategoryStatus Status { get; set; }
        public virtual ICollection<CourseCategory> CourseCategories { get; set; } = new List<CourseCategory>();
    }
}

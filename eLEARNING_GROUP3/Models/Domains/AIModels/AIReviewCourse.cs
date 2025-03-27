using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.Domains.AIModels
{
    public class AIReviewCourse
    {
        [Key]
        public long AIReviewId { get; set; }
        public long CourseId { get; set; }
        [Required]
        public float AIConfidence { get; set; }
        [StringLength(20), Required]
        public ReviewStatus Status { get; set; }
        public string? ReviewNote { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Course Course { get; set; } = null!;
    }
}

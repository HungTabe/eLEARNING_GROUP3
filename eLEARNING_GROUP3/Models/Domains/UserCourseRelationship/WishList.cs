using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.UserCourseRelationship
{
    public class WishList
    {
        public long CourseId { get; set; }
        public long UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual Course Course { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

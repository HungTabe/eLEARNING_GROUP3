using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
    public class WishlistDTO
    {
        public long CourseId { get; set; }

        [Required]
        [StringLength(255)]
        public string CourseName { get; set; } = null!;

        //public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        //[Column(TypeName = "decimal(18,2)")]
        //public decimal? Discount { get; set; }

        [Required]
        public long Creator { get; set; }

        public string Description { get; set; } = "";
        public List<string> CourseUrls { get; set; } = new List<string>();
    }
}

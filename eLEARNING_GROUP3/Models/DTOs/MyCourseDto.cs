using OnlineLearning.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
    public class MyCourseDto
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

        //public long? Acceptor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //public DateTime? UpdatedAt { get; set; }

        //public DateTime? PublishedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }

        //[StringLength(50)]
        //public string? StudyTime { get; set; }

        //public long? LanguageId { get; set; }

        //public long? LevelId { get; set; }

        //[Required]
        //public CourseStatus Status { get; set; } = CourseStatus.Draft;

        public string Description { get; set; } = "";
        public List<string> CourseUrls { get; set; } // Thay đổi từ string thành List<string>

        public int Progress { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Models.DTOs
{
    public class Admin_ReviewCourseDto
    {

        
        public long CourseId { get; set; }
        public long AdminId { get; set; }


        [Required]
        [StringLength(255)]
        public string CourseName { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Discount { get; set; }

  
        public long CreatorId { get; set; }
        public string? CreatorName { get; set; }
        public string? ReviewNote { get; set; }

        //public long? Acceptor { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime ReviewdAt { get; set; } = DateTime.Now;

        //public DateTime? UpdatedAt { get; set; }

        //public DateTime? PublishedAt { get; set; }

        //public DateTime? DeletedAt { get; set; }

        [StringLength(50)]
        public string? StudyTime { get; set; }

        public long? LanguageId { get; set; }

        public long? LevelId { get; set; }

        [Required]
        public CourseStatus Status { get; set; } = CourseStatus.Draft;

        public string CourseImgUrl { get; set; } = "";
        public List<CourseImageUrl> CourseUrls { get; set; } // Thay đổi từ string thành List<string>
        public long? CategoryId { get; set; }


    }
}

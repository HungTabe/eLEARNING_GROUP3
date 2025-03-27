using OnlineLearning.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
    public class CourseDto
    {
        [Required(ErrorMessage = "CourseName không được để trống!")]
        [MaxLength(50, ErrorMessage = "CourseName không được vượt quá 50 ký tự!")]

        public string CourseName { get; set; }

        [Required(ErrorMessage = "CourseUrl không được để trống!")]
        public string CourseUrl { get; set; } 


        public string? Description { get; set; }

        [Required(ErrorMessage = "Price không được để trống!")]
        [Range(0, double.MaxValue, ErrorMessage = "Price không được âm!")]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount không được âm!")]
        public decimal? Discount { get; set; }

        public long? LanguageId { get; set; }

        public long? LevelId { get; set; }


        public long CategoryId { get; set; }

    }

}

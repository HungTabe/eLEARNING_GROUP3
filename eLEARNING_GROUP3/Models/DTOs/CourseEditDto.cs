using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace OnlineLearning.Models.DTOs
{
    public class CourseEditDto
    {
        public long CourseId { get; set; }

        [Required(ErrorMessage = "CourseName không được để trống!")]
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
        public long? OldCategoryId { get; set; }
    }
}

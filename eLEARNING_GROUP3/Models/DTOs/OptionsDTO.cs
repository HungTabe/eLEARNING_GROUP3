using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace OnlineLearning.Models.DTOs
{
    public class OptionsDTO
    {
        //[Required]
        public long OptionId { get; set; }

        //[Required]
        public long QuestionId { get; set; }

        [Required(ErrorMessage = "Option Text Cannot Be Left Blank")]
        [StringLength(255, ErrorMessage = "Option Text cannot exceed 255 characters.")]
        public string OptionText { get; set; } = null!; // Dùng null! để tránh cảnh báo nullable

        public int IsCorrect { get; set; }
    }
}

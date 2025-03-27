using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
    public class QuizDTO
    {
        //[Required]
        public long QuizId { get; set; }

        //[Required]
        public long ModuleId { get; set; }

        [Required(ErrorMessage = "Quiz name not blank.")]
        [StringLength(255, ErrorMessage = "Quiz name cannot exceed 255 characters.")]
        public string QuizName { get; set; } = null!; // Dùng null! để tránh cảnh báo nullable

        [Required(ErrorMessage = "Quiz time not blank.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quiz time must be greater than 0.")]
        public int QuizTime { get; set; }

        public string TimeUnit { get; set; }

        [Required(ErrorMessage = "Pass score not blank.")]
        [Range(1, 10, ErrorMessage = "Pass score must be greater than 0.")]
        public int PassScore { get; set; }



    }
}

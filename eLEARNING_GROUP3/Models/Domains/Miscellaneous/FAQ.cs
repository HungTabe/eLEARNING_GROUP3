using System.ComponentModel.DataAnnotations;
using OnlineLearning.Enums;

namespace OnlineLearning.Models.Domains.Miscellaneous
{
    public class FAQ
    {
        [Key]
        public long FaqId { get; set; }
        [Required]
        public string Question { get; set; } = null!;
        [Required]
        public string Answer { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public CommonStatus CommonStatus { get; set; } = CommonStatus.Showed;
    }
}

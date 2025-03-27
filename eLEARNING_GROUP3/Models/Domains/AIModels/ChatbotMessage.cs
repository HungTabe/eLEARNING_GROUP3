using System.ComponentModel.DataAnnotations;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.Domains.AIModels
{
    public class ChatbotMessage
    {
        [Key]
        public long AIChatId { get; set; }
        public long UserId { get; set; }
        [Required]
        public string BotResponse { get; set; } = null!;
        [Required]
        public string UserQuery { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual User User { get; set; } = null!;
    }
}

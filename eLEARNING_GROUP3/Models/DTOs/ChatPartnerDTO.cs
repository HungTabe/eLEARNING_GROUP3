using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.Domains.UserModels;

namespace OnlineLearning.Models.DTOs
{
    public class ChatPartnerDTO
    {
        public User Partner { get; set; } = null!;
        public Message LatestMessage { get; set; } = null!;
        public int UnreadCount { get; set; }
    }
}

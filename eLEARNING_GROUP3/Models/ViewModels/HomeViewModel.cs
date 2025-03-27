using OnlineLearning.Models.Domains.AIModels;

namespace OnlineLearning.Models.ViewModels
{
    public class HomeViewModel
    {
        public long? UserId { get; set; }
        public IEnumerable<ChatbotMessage> ChatbotMessage { get; set; } = new List<ChatbotMessage>();
    }
}

using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Models.Domains.Miscellaneous;

namespace OnlineLearning.Services.Interfaces.AI
{
    public interface IChatbotService
    {
        Task<string> GetChatbotResponseAsync(string userQuery);
        Task<ChatbotMessage> SendMessageAsync(long userId, string userQuery);
        Task<IEnumerable<ChatbotMessage>> GetHistoryChatAsyncByUserId(long userId);
    }
}

using OnlineLearning.Models.Domains.AIModels;

namespace OnlineLearning.Repositories.Interfaces.AI
{
    public interface IChatbotRepository : IBaseRepository<ChatbotMessage>
    {
        Task<IEnumerable<ChatbotMessage>> GetMessagesByUserIdAsync(long userId);
    }
}

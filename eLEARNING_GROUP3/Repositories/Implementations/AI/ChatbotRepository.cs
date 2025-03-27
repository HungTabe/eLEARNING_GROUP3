using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Repositories.Interfaces.AI;

namespace OnlineLearning.Repositories.Implementations.AI
{
    public class ChatbotRepository : BaseRepository<ChatbotMessage>, IChatbotRepository
    {
        public ChatbotRepository(OnlLearnDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ChatbotMessage>> GetMessagesByUserIdAsync(long userId)
        {
            return await _context.ChatbotMessages.Where(m => m.UserId == userId).
                OrderBy(m => m.CreatedAt).ToListAsync();
        }
    }
}

using OnlineLearning.Models.Domains.Miscellaneous;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<Message> AddMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long senderId, long receiverId);
        Task<IEnumerable<Message>> GetLatestMessagesForUserAsync(long userId);
        Task MarkMessagesAsReadAsync(long senderId, long receiverId);
        Task<int> CountUnreadMessagesAsync(long senderId, long receiverId);
    }
}

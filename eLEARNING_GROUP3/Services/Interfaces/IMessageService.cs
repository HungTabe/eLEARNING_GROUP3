using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> SendMessageAsync(long senderId, long receiverId, string content);
        Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long senderId, long receiverId);
        Task<IEnumerable<ChatPartnerDTO>> GetChatPartnersAsync(long userId);
        Task MarkMessagesAsReadAsync(long senderId, long receiverId);
    }
}

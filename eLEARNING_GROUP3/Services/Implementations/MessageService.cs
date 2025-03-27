using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        public async Task<Message> SendMessageAsync(long senderId, long receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            return await _messageRepository.AddMessageAsync(message);
        }

        public async Task<IEnumerable<Message>> GetMessagesBetweenUsersAsync(long senderId, long receiverId)
        {
            return await _messageRepository.GetMessagesBetweenUsersAsync(senderId, receiverId);
        }

        public async Task<IEnumerable<ChatPartnerDTO>> GetChatPartnersAsync(long userId)
        {
            // Lấy danh sách tin nhắn mới nhất
            var latestMessages = await _messageRepository.GetLatestMessagesForUserAsync(userId);
            var chatPartners = new List<ChatPartnerDTO>();

            foreach (var message in latestMessages)
            {
                // Xác định partner là người gửi hoặc người nhận
                var partnerId = message.SenderId == userId ? message.ReceiverId : message.SenderId;
                var partner = await _userRepository.GetByIdAsync(partnerId);

                if (partner != null)
                {
                    // Đếm số tin nhắn chưa đọc sử dụng phương thức từ repository
                    var unreadCount = await _messageRepository.CountUnreadMessagesAsync(partnerId, userId);

                    chatPartners.Add(new ChatPartnerDTO
                    {
                        Partner = partner,
                        LatestMessage = message,
                        UnreadCount = unreadCount
                    });
                }
            }

            return chatPartners;
        }

        public async Task MarkMessagesAsReadAsync(long senderId, long receiverId)
        {
            await _messageRepository.MarkMessagesAsReadAsync(senderId, receiverId);
        }
    }
}

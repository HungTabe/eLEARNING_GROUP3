using Microsoft.AspNetCore.SignalR;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Hubs
{
    public class UserChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public UserChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task SendMessageToUser(long senderId, long receiverId, string content)
        {
            // Lưu tin nhắn vào cơ sở dữ liệu
            var message = await _messageService.SendMessageAsync(senderId, receiverId, content);
            
            // Gửi tin nhắn đến người nhận và người gửi
            await Clients.Group(receiverId.ToString()).SendAsync("ReceiveMessage", message);
            await Clients.Group(senderId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinUserGroup(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        public async Task LeaveUserGroup(string userId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
        }

        public async Task MarkMessagesAsRead(long senderId, long receiverId)
        {
            await _messageService.MarkMessagesAsReadAsync(senderId, receiverId);
            
            // Thông báo cho người gửi rằng tin nhắn đã được đọc
            await Clients.Group(senderId.ToString()).SendAsync("MessagesRead", receiverId);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using OnlineLearning.Services.Interfaces.AI;

namespace OnlineLearning.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatbotService _chatbotService;

        public ChatHub(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public async Task SendMessage(long userId, string userQuery)
        {
            var chatbotMessage = await _chatbotService.SendMessageAsync(userId, userQuery);
            await Clients.All.SendAsync("ReceiveMessage", chatbotMessage.UserId, chatbotMessage.UserQuery, chatbotMessage.BotResponse);
        }
    }
}

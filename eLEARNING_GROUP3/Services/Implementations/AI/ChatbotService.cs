using Microsoft.IdentityModel.Tokens;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Repositories.Interfaces.AI;
using OnlineLearning.Services.Interfaces.AI;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineLearning.Services.Implementations.AI
{
    public class ChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IChatbotRepository _chatbotRepository;
        private readonly IAITrainingRepository _aiTrainingRepository;

        public ChatbotService(HttpClient httpClient, IConfiguration configuration, IChatbotRepository chatbotMessageRepository, IAITrainingRepository aiTrainingRepository)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _chatbotRepository = chatbotMessageRepository;
            _aiTrainingRepository = aiTrainingRepository;
        }
        //Khi sử dụng Moq(TEST) để tạo mock một class (Mock<ClassName>), Moq cần ghi đè (override) phương thức để thay đổi cách hoạt động của nó trong unit test.
        public virtual async Task<string> GetChatbotResponseAsync(string userQuery)
        {
            // 1. Kiểm tra dữ liệu training trước
            var trainedResponse = await _aiTrainingRepository.FindResponseAsync(userQuery);
            if (!string.IsNullOrEmpty(trainedResponse))
            {
                return trainedResponse; // Trả lời nếu có dữ liệu training
            }

            // 2. Nếu không có, gọi OpenAI API
            var apiKey = _configuration["OpenAI:ApiKey"];
            var request = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = userQuery } },
                max_tokens = 200
            };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", request);
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "";
        }

        public async Task<ChatbotMessage> SendMessageAsync(long userId, string userQuery)
        {
            var response = await GetChatbotResponseAsync(userQuery);
            var chatbotMessage = new ChatbotMessage { UserId = userId, UserQuery = userQuery, BotResponse = response };
            await _chatbotRepository.AddAsync(chatbotMessage);
            return chatbotMessage;
        }

        public async Task<IEnumerable<ChatbotMessage>> GetHistoryChatAsyncByUserId(long userId)
        {
            var list = await _chatbotRepository.GetMessagesByUserIdAsync(userId);

            if (list.IsNullOrEmpty())
            {
                var botHello = new ChatbotMessage
                {
                    BotResponse = "Hello. How can I help you today?",
                    UserId = userId,
                    CreatedAt = DateTime.Now
                };
                return new List<ChatbotMessage> { botHello };
            }
            return list;

        }
    }
}

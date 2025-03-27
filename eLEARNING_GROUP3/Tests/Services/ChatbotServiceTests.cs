using Moq;
using Moq.Protected;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Repositories.Interfaces.AI;
using OnlineLearning.Services.Implementations.AI;
using OnlineLearning.Services.Interfaces;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit;

namespace OnlineLearning.Tests.Services
{
    public class ChatbotServiceTests
    {
        private readonly Mock<IChatbotRepository> _chatbotRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly ChatbotService _chatbotService;
        private readonly Mock<IAITrainingRepository> _aiTrainingMock;

        public ChatbotServiceTests()
        {
            _chatbotRepositoryMock = new Mock<IChatbotRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _aiTrainingMock = new Mock<IAITrainingRepository>();

            _chatbotService = new ChatbotService(_httpClient, _configurationMock.Object, _chatbotRepositoryMock.Object, _aiTrainingMock.Object);
        }

        [Fact]
        //Kiểm Tra API Trả Về Kết Quả
        public async Task GetChatbotResponseAsync_ShouldReturnResponse_WhenApiReturnsValidData()
        {
            // Arrange
            string userQuery = "Hello";
            string botResponse = "Hello, how can I assist you?";
            var jsonResponse = JsonSerializer.Serialize(new
            {
                choices = new[]
                {
                new { message = new { content = botResponse } }
            }
            });

            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            _configurationMock.Setup(c => c["OpenAI:ApiKey"]).Returns("fake-api-key");

            // Act
            var result = await _chatbotService.GetChatbotResponseAsync(userQuery);

            // Assert
            Assert.Equal(botResponse, result);
        }

        [Fact]
        //Kiểm Tra Gửi Tin Nhắn (SendMessageAsync)
        public async Task SendMessageAsync_ShouldSaveMessageAndReturnChatbotMessage()
        {
            // Arrange
            long userId = 1;
            string userQuery = "What is AI?";
            string botResponse = "AI stands for Artificial Intelligence.";

            _chatbotRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ChatbotMessage>()))
                                  .ReturnsAsync((ChatbotMessage msg) => msg);

            var chatbotServiceMock = new Mock<ChatbotService>(_httpClient, _configurationMock.Object, _chatbotRepositoryMock.Object);
            chatbotServiceMock.Setup(service => service.GetChatbotResponseAsync(userQuery))
                              .ReturnsAsync(botResponse);

            // Act
            var result = await chatbotServiceMock.Object.SendMessageAsync(userId, userQuery);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(userQuery, result.UserQuery);
            Assert.Equal(botResponse, result.BotResponse);
            _chatbotRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ChatbotMessage>()), Times.Once);
        }


        [Fact]
        //Kiểm Tra Lịch Sử Tin Nhắn (GetHistoryChatAsyncByUserId)
        public async Task GetHistoryChatAsyncByUserId_ShouldReturnMessages()
        {
            // Arrange
            long userId = 1;
            var messages = new List<ChatbotMessage>
        {
            new ChatbotMessage { AIChatId = 1, UserId = userId, UserQuery = "Hi", BotResponse = "Hello, how can I assist you?" },
            new ChatbotMessage { AIChatId = 2, UserId = userId, UserQuery = "What is AI?", BotResponse = "AI stands for Artificial Intelligence." }
        };

            _chatbotRepositoryMock.Setup(repo => repo.GetMessagesByUserIdAsync(userId))
                                  .ReturnsAsync(messages);

            // Act
            var result = await _chatbotService.GetHistoryChatAsyncByUserId(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, msg => msg.UserQuery == "Hi");
            Assert.Contains(result, msg => msg.BotResponse == "AI stands for Artificial Intelligence.");
        }
    }
}

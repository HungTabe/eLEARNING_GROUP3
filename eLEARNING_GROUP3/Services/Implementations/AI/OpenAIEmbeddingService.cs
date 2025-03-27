using OnlineLearning.Services.Interfaces.AI;
using System.Net.Http.Headers;
using System.Text.Json;

namespace OnlineLearning.Services.Implementations.AI
{
    public class OpenAIEmbeddingService : IOpenAIEmbeddingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OpenAIEmbeddingService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<float[]> GetEmbeddingAsync(string text)
        {
            var apiKey = _configuration["OpenAI:ApiKey"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var request = new { input = text, model = "text-embedding-ada-002" };
            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/embeddings", request);
            var result = await response.Content.ReadFromJsonAsync<JsonElement>();

            return result.GetProperty("data")[0].GetProperty("embedding").EnumerateArray()
                .Select(x => x.GetSingle()).ToArray();
        }
    }
}

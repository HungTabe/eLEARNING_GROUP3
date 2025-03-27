using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Repositories.Interfaces.AI;
using OnlineLearning.Services.Interfaces.AI;

namespace OnlineLearning.Repositories.Implementations.AI
{
    public class AITrainingRepository : BaseRepository<ChatbotMessage>, IAITrainingRepository
    {
        private readonly IOpenAIEmbeddingService _embeddingService;

        public AITrainingRepository(OnlLearnDBContext context, IOpenAIEmbeddingService embeddingService) : base(context)
        {
            _embeddingService = embeddingService;
        }

        public async Task<string?> FindResponseAsync(string userQuery)
        {
            var queryVector = await _embeddingService.GetEmbeddingAsync(userQuery);
            var trainingData = await _context.AITrainingData
                .Where(data => data.VectorEmbedding != null)
                .ToListAsync();

            var bestMatch = trainingData
                .Select(data => new
                {
                    Data = data,
                    Similarity = CosineSimilarity(queryVector, data.VectorEmbedding!)
                })
                .OrderByDescending(x => x.Similarity)
                .FirstOrDefault(x => x.Similarity > 0.85); // Chỉ trả lời nếu độ tương đồng > 85%

            return bestMatch?.Data.ExpectedOutput;
        }
        private double CosineSimilarity(float[] vectorA, float[] vectorB)
        {
            double dotProduct = vectorA.Zip(vectorB, (a, b) => a * b).Sum();
            double magnitudeA = Math.Sqrt(vectorA.Sum(a => a * a));
            double magnitudeB = Math.Sqrt(vectorB.Sum(b => b * b));

            return dotProduct / (magnitudeA * magnitudeB);
        }
        public async Task AddTrainingDataWithEmbeddingAsync(string inputText, string expectedOutput)
        {
            var vector = await _embeddingService.GetEmbeddingAsync(inputText);

            var newData = new AITrainingData
            {
                InputText = inputText,
                ExpectedOutput = expectedOutput,
                VectorEmbedding = vector, // Lưu vector embedding
                CreatedAt = DateTime.Now
            };

            await _context.AITrainingData.AddAsync(newData);
            await _context.SaveChangesAsync();
        }
    }
}

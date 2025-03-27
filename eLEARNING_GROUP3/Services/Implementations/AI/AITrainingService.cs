using OnlineLearning.Repositories.Interfaces.AI;
using OnlineLearning.Services.Interfaces.AI;

namespace OnlineLearning.Services.Implementations.AI
{
    public class AITrainingService : IAITrainingService
    {
        private readonly IAITrainingRepository _aiTrainingRepository;

        public AITrainingService(IAITrainingRepository aiTrainingRepository)
        {
            _aiTrainingRepository = aiTrainingRepository;
        }
        public async Task AddTrainingDataWithEmbeddingAsync(string inputText, string expectedOutput)
        {
            await _aiTrainingRepository.AddTrainingDataWithEmbeddingAsync(inputText, expectedOutput);
        }
    }
}
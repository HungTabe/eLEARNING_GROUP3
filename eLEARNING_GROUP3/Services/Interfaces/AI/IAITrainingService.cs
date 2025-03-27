namespace OnlineLearning.Services.Interfaces.AI
{
    public interface IAITrainingService
    {
        Task AddTrainingDataWithEmbeddingAsync(string inputText, string expectedOutput);
    }
}

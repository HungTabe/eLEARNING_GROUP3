namespace OnlineLearning.Repositories.Interfaces.AI
{
    public interface IAITrainingRepository
    {
        Task<string?> FindResponseAsync(string userQuery);
        Task AddTrainingDataWithEmbeddingAsync(string inputText, string expectedOutput);
    }
}

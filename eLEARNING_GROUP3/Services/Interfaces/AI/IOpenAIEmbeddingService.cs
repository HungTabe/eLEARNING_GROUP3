namespace OnlineLearning.Services.Interfaces.AI
{
    public interface IOpenAIEmbeddingService
    {
        Task<float[]> GetEmbeddingAsync(string text);
    }
}

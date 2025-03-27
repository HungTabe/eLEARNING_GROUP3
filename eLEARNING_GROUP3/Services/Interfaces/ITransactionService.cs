using OnlineLearning.Models.Domains.Miscellaneous;

namespace OnlineLearning.Services.Interfaces
{
    public interface ITransactionService
    {
        public Task<bool> AddTransactionAsync(TransactionHistory transaction);
    }
}

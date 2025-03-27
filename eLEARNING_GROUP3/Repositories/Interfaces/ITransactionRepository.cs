using OnlineLearning.Models.Domains.Miscellaneous;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<TransactionHistory>
    {
        Task<TransactionHistory> GetTransactionByUserIdAndCourseId(long userId, long? coueseId);
    }
}

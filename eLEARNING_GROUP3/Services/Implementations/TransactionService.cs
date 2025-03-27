using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepo;
        public TransactionService(ITransactionRepository transactionRepository) { 
            _transactionRepo = transactionRepository;
        }
        public async Task<bool> AddTransactionAsync(TransactionHistory transaction)
        {
            var getTransaction = await _transactionRepo.GetTransactionByUserIdAndCourseId(transaction.UserId, transaction.CourseId);

            if (getTransaction != null) {
                throw new Exception("The course has been paid for.");
            }
            var trs = await _transactionRepo.AddAsync(transaction) ;
            return trs != null;
        }
    }
}

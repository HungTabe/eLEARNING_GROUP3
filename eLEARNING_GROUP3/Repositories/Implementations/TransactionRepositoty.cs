using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class TransactionRepositoty : BaseRepository<TransactionHistory>, ITransactionRepository
    {
        public TransactionRepositoty(OnlLearnDBContext context) : base(context) { }

        public async Task<TransactionHistory> GetTransactionByUserIdAndCourseId(long userId, long? coueseId)
        {
            return await _context.TransactionHistories.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == coueseId);
        }
    }
}

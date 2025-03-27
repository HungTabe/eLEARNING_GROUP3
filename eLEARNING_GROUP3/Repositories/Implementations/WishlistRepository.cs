using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class WishlistRepository : BaseRepository<WishList>, IWishlistRepository
    {
        public WishlistRepository(OnlLearnDBContext context) : base(context) { }

        public async Task<WishList> ExistingWishListAsync(long userId, long courseId)
        {
             return await _context.WishLists.FirstOrDefaultAsync(x => x.UserId == userId && x.CourseId == courseId);
        }
    }
}

using OnlineLearning.Models.Domains.UserCourseRelationship;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface IWishlistRepository :IBaseRepository<WishList>
    {
        Task<WishList> ExistingWishListAsync(long userId, long courseId);
    }
}

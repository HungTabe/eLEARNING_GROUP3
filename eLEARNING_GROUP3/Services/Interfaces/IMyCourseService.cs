using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
    public interface IMyCourseService
    {
        Task<List<MyCourseDto>> GetMyCourseList(long userId);
        Task<List<WishlistDTO>> GetMyWishlist(long userId);
        Task<ResultDto> AddToWishlistAsync(long userId, long courseId);
    }
}

using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Repositories.Interfaces

{
    public interface IMyCourseRepository : IBaseRepository<Course>
    {
        Task<List<MyCourseDto>> GetCourseAndProgressById(long id);
        Task<List<WishlistDTO>> GetMyWishListByUserId(long userId);
    }
}

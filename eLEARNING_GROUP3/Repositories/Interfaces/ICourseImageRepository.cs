using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface ICourseImageRepository : IBaseRepository<CourseImageUrl>
    {
        Task<CourseImageUrl> GetByCourseIdAsync(long courseId);
        Task<List<CourseImageUrl>> GetListImgByCourseIdAsync(long courseId);

    }
}

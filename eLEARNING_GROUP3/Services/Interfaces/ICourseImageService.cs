using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface ICourseImageService 
    {
        Task<CourseImageUrl> AddCourseImageUrlAsync(CourseImageUrl courseImageUrl);

        Task UpdateCourseImageUrAsync(CourseImageUrl courseImageUrl);


        Task<CourseImageUrl> GetByCourseIdAsync(long courseId); 
        Task<List<CourseImageUrl>> GetListImgByCourseIdAsync(long courseId);

    }
}

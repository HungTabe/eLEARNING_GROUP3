using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface ICourseCategoriesService
    {
        Task<CourseCategory> AddCourseCategoryAsync(CourseCategory courseCategory);
        Task<List<Category>> GetCategoriesByCourseIdAsync(long courseId);
        Task<long?> GetCategoryIdsByCourseIdAsync(long courseId);
        Task<List<CourseCategory>> GetCourseCategoriesByCourseIdAsync(long courseId);

        Task UpdateCourseCategoriesAsync(CourseCategory courseCategory);

        Task DeleteCourseCategoryAsync(CourseCategory courseCategory);
        Task<CourseCategory> GetByCourseIdAsync(long courseId, long categoryId);

    }
}

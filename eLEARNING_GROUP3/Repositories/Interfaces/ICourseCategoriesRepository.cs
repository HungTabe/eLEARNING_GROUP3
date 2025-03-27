using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface ICourseCategoriesRepository : IBaseRepository<CourseCategory>
    {
        Task<List<Category>> GetCategoriesByCourseIdAsync(long courseId);
        Task<List<CourseCategory>> GetCourseCategoriesByCourseIdAsync(long courseId);
        Task<long?> GetCategoryIdByCourseIdAsync(long courseId);
	}
}

using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoryAysnc();

    }
}

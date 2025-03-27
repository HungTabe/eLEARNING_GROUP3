using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

      
        public async Task<IEnumerable<Category>> GetAllCategoryAysnc()
        {
            return await _categoryRepository.GetAllAsync();
        }
    }
}

using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(OnlLearnDBContext context) : base(context)
        {
        }
    }
}

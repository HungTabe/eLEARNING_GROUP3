using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class LevelRepository : BaseRepository<Level>, ILevelRepository
    {
        public LevelRepository(OnlLearnDBContext context) : base(context)
        {
        }
    }
}

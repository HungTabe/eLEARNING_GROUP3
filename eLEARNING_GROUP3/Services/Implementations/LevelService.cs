using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Repositories.Implementations;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class LevelService : ILevelService
    {
        private readonly ILevelRepository _levelRepository;

        public LevelService(ILevelRepository levelRepository)
        {
            _levelRepository = levelRepository;
        }

        public async Task<IEnumerable<Level>> GetAllLevelAysnc()
        {
            return await _levelRepository.GetAllAsync();
        }

       
    }
}

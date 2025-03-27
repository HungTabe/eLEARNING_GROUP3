using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<Module> AddModuleAsync(Module module)
        {
            return await _moduleRepository.AddAsync(module);
        }

        public async Task DeleteModuleAsync(Module module)
        {
            await _moduleRepository.DeleteAsync(module);
        }

        public async Task<IEnumerable<Module>> GetAllModuleByCourseId(long courseId)
        {
            return await _moduleRepository.GetAllModuleByCourseIdAysnc(courseId);
        }

        public async Task<Module> GetModuleAsync(long courseId, int moduleNumber)
        {
            return await _moduleRepository.GetModuleAsync(courseId, moduleNumber);
        }

		
		public async Task<int> GetNextModuleNumberAsync(long courseId)
        {
            return await _moduleRepository.GetNextModuleNumberAsync(courseId);
        }

        public async Task UpdateModuleAsync(Module module)
        {
            await _moduleRepository.UpdateAsync(module);
        }
		public async Task<Module> GetModuleByIdAsync(long moduleId)
		{
			return await _moduleRepository.GetByIdAsync(moduleId);
		}
	}
}

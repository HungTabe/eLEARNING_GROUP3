using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface IModuleService 
    {
        Task<Module> AddModuleAsync(Module module);

        Task<IEnumerable<Module>> GetAllModuleByCourseId(long courseId);

        Task<int> GetNextModuleNumberAsync(long courseId);

        Task UpdateModuleAsync(Module module);

        Task<Module> GetModuleAsync(long courseId, int moduleNumber);

        Task DeleteModuleAsync(Module module);

		Task<Module> GetModuleByIdAsync(long moduleId);
	}
}

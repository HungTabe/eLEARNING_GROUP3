using OnlineLearning.Models.Domains.CourseModels;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface IModuleRepository : IBaseRepository<Module>
    {
        Task<IEnumerable<Module>> GetAllModuleByCourseIdAysnc(long courseId);

        Task<int> GetNextModuleNumberAsync(long courseId);


        Task<Module> GetModuleAsync(long courseId, int moduleNumber);


    }
}

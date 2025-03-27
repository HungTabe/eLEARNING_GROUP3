using OnlineLearning.Models.Domains.CourseModels.LessonModels;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface ILessonRepository : IBaseRepository<Lesson>
    {
        Task<IEnumerable<Lesson>> GetAllLessonByModuleIdAsync(long moduleId);
        Task<int> GetNextLessonNumberAsync(long moduleId);

    }
}

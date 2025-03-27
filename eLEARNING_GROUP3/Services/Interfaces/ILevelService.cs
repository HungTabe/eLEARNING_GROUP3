using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface ILevelService 
    {
        Task<IEnumerable<Level>> GetAllLevelAysnc();
    }
}

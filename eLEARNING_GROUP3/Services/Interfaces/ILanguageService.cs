using OnlineLearning.Models.Domains.CourseModels.CategoryModels;

namespace OnlineLearning.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<Language>> GetAllLanguageAysnc();
    }

}

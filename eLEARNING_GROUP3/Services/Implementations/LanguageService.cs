using Microsoft.CodeAnalysis.Host;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Implementations;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class LanguageService : OnlineLearning.Services.Interfaces.ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public async Task<IEnumerable<Language>> GetAllLanguageAysnc()
        {
            return await _languageRepository.GetAllAsync();
        }
      
    }

}

using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
    public interface IFAQsService
    {
        Task AddAsync(FAQ faq);
        Task<IEnumerable<FAQ>> GetAllAsync();
        Task<FAQ?> GetByIdAsync(long id);
        Task UpdateAsync(FAQ faq);
        Task DeleteAsync(FAQ faq);
        Task<bool> IsFAQExists(long id);
        Task<IEnumerable<FAQsDTO>> GetListFAQsToView();
    }
}

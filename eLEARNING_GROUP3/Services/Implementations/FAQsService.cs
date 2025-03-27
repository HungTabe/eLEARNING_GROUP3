using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Implementations;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class FAQsService : IFAQsService
    {
        private readonly IFAQsRepository _faqsRepository;
        public FAQsService(IFAQsRepository faqsRepository)
        {
            _faqsRepository = faqsRepository;
        }

        public async Task AddAsync(FAQ faq)
        {
            faq.CreatedAt = DateTime.Now;
            faq.UpdatedAt = DateTime.Now;
            await _faqsRepository.AddAsync(faq);
        }

        public async Task DeleteAsync(FAQ faq)
        {
            await _faqsRepository.DeleteAsync(faq);
        }

        public async Task<IEnumerable<FAQ>> GetAllAsync()
        {
            return await _faqsRepository.GetAllAsync();
        }

        public async Task<FAQ?> GetByIdAsync(long id)
        {
            return await _faqsRepository.GetByIdAsync(id);
        }

        public async Task<bool> IsFAQExists(long id)
        {
            var faq = await GetByIdAsync(id);
            return faq != null;
        }


        public async Task UpdateAsync(FAQ faq)
        {
            var existingFaq = await _faqsRepository.GetByIdAsync(faq.FaqId);
            if (existingFaq == null)
            {
                throw new Exception("FAQ not found");
            }
            existingFaq.Question = faq.Question;
            existingFaq.Answer = faq.Answer;
            existingFaq.CommonStatus = faq.CommonStatus;
            existingFaq.UpdatedAt = DateTime.Now;

            await _faqsRepository.UpdateAsync(existingFaq);
        }
        public async Task<IEnumerable<FAQsDTO>> GetListFAQsToView()
        {
            var faqs = await _faqsRepository.GetAllAsync();
            return faqs.Select(f => new FAQsDTO
            {
                Id = f.FaqId,
                Question = f.Question,
                Answer = f.Answer
            }).ToList();
        }
    }
}

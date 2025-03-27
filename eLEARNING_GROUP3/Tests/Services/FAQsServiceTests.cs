using Moq;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Implementations;
using Xunit;

namespace OnlineLearning.Tests.Services
{
    public class FAQsServiceTests
    {
        private readonly Mock<IFAQsRepository> _faqsRepositoryMock;
        private readonly FAQsService _faqsService;

        public FAQsServiceTests()
        {
            _faqsRepositoryMock = new Mock<IFAQsRepository>();
            _faqsService = new FAQsService(_faqsRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldCallRepositoryAddAsync()
        {
            
            var faq = new FAQ { FaqId = 1, Question = "Question 1?", Answer = "Answer 1" };

            
            await _faqsService.AddAsync(faq);

            
            _faqsRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<FAQ>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteAsync()
        {
            
            var faq = new FAQ { FaqId = 1 };

            
            await _faqsService.DeleteAsync(faq);

            
            _faqsRepositoryMock.Verify(repo => repo.DeleteAsync(faq), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnFAQs()
        {
            
            var faqs = new List<FAQ>
        {
            new FAQ { FaqId = 1, Question = "Q1", Answer = "A1" },
            new FAQ { FaqId = 2, Question = "Q2", Answer = "A2" }
        };
            _faqsRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(faqs);

            
            var result = await _faqsService.GetAllAsync();

            
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnFAQ_WhenExists()
        {
            
            var faq = new FAQ { FaqId = 1, Question = "Question 1?", Answer = "Answer 1." };
            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(faq);

            
            var result = await _faqsService.GetByIdAsync(1);

            
            Assert.NotNull(result);
            Assert.Equal("Question 1?", result.Question);
            Assert.Equal("Answer 1.", result.Answer);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
        {
            
            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((FAQ?)null);

            
            var result = await _faqsService.GetByIdAsync(999);

            
            Assert.Null(result);
        }

        [Fact]
        public void IsFAQExists_ShouldReturnTrue_WhenFAQExists()
        {
            
            var faq = new FAQ { FaqId = 1, Question = "Q1?", Answer = "A1." };
            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(faq);

            
            var result = _faqsService.IsFAQExists(1).GetAwaiter().GetResult();

            Assert.True(result);
        }

        [Fact]
        public void IsFAQExists_ShouldReturnFalse_WhenFAQDoesNotExist()
        {
            
            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((FAQ?)null);

            var result = _faqsService.IsFAQExists(999).GetAwaiter().GetResult();

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateFAQ_WhenExists()
        {
            
            var existingFaq = new FAQ { FaqId = 1, Question = "Old Question", Answer = "Old Answer" };
            var updatedFaq = new FAQ { FaqId = 1, Question = "New Question", Answer = "New Answer" };

            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(existingFaq);
            _faqsRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<FAQ>())).Returns(Task.CompletedTask);

            
            await _faqsService.UpdateAsync(updatedFaq);

            
            Assert.Equal("New Question", existingFaq.Question);
            Assert.Equal("New Answer", existingFaq.Answer);
            _faqsRepositoryMock.Verify(repo => repo.UpdateAsync(existingFaq), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenFAQNotExists()
        {
            var updatedFaq = new FAQ { FaqId = 1, Question = "New Question", Answer = "New Answer" };

            _faqsRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((FAQ?)null);

            await Assert.ThrowsAsync<Exception>(() => _faqsService.UpdateAsync(updatedFaq));
        }
    }
}

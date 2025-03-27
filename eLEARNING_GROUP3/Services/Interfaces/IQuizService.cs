using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
    public interface IQuizService
    {
        Task<Quiz> CreateQuizAsync(QuizDTO quizDTO);
        Task UpdateQuizAsync(QuizDTO quizDTO);
        Task DeleteQuizAsync(Quiz quiz);
        Task<IEnumerable<QuizDTO>> GetAllQuizAsync();
    }
}

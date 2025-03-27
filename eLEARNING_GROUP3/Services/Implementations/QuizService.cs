using Humanizer.Localisation;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Quiz> CreateQuizAsync(QuizDTO quizDTO)
        {
            int convertedTime = quizDTO.QuizTime;
            if (quizDTO.TimeUnit == "minutes")
            {
                convertedTime = quizDTO.QuizTime * 60; // Chuyển phút sang giây
            }
            var quiz = new Quiz
            {
                
                QuizName = quizDTO.QuizName,
                ModuleId = quizDTO.ModuleId,
                QuizTime = convertedTime,
                PassScore = quizDTO.PassScore,
                Status = QuizStatus.Active
            };

            return await _quizRepository.AddAsync(quiz);
        }

        public async Task UpdateQuizAsync(QuizDTO quizDTO)
        {
            var quiz = new Quiz
            {
                QuizId = quizDTO.QuizId,
                QuizName = quizDTO.QuizName,
                ModuleId = quizDTO.ModuleId,
                QuizTime = quizDTO.QuizTime,
                PassScore = quizDTO.PassScore,
                UpdatedAt = DateTime.Now
            };

            await _quizRepository.UpdateAsync(quiz);
        }

        public async Task DeleteQuizAsync(Quiz quiz)
        {
            await _quizRepository.DeleteAsync(quiz);
        }

        public async Task<IEnumerable<QuizDTO>> GetAllQuizAsync()
        {
            var quizzes = await _quizRepository.GetAllAsync();
            return quizzes.Select(q => new QuizDTO
            {
                QuizId = q.QuizId,
                QuizName = q.QuizName,
                ModuleId = q.ModuleId,
                QuizTime = q.QuizTime ?? 0,
                PassScore = q.PassScore ?? 0
            });
        }
    }
}

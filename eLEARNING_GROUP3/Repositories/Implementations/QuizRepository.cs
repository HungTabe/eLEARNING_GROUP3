using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels.QuizModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class QuizRepository : BaseRepository<Quiz>, IQuizRepository
    {
        public QuizRepository(OnlLearnDBContext context) : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class LessonRepository : BaseRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(OnlLearnDBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Lesson>> GetAllLessonByModuleIdAsync(long moduleId)
        {
            return await _context.Lessons
                .Where(l => l.ModuleId == moduleId)
                .OrderBy(l => l.LessonNumber)
                .ToListAsync();
        }
        public async Task<int> GetNextLessonNumberAsync(long moduleId)
        {
            var existingLessons = await _context.Lessons
                .Where(l => l.ModuleId == moduleId)
                .OrderBy(l => l.LessonNumber)
                .ToListAsync();

            int nextLessonNumber = 1; // Mặc định nếu chưa có Lesson nào

            if (existingLessons.Any())
            {
                var lessonNumbers = existingLessons.Select(l => l.LessonNumber).ToList();

                for (int i = 1; i <= lessonNumbers.Count + 1; i++)
                {
                    if (!lessonNumbers.Contains(i))
                    {
                        nextLessonNumber = i;
                        break;
                    }
                }
            }

            return nextLessonNumber;
        }

    }
}

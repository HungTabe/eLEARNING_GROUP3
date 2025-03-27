using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class CourseImageRepository : BaseRepository<CourseImageUrl>, ICourseImageRepository
    {
        public CourseImageRepository(OnlLearnDBContext context) : base(context)
        {
        }

        public async Task<CourseImageUrl> GetByCourseIdAsync(long courseId)
        {
            return await _context.CourseImageUrls.FirstOrDefaultAsync(ci => ci.CourseId == courseId);
        }

        public async Task<List<CourseImageUrl>> GetListImgByCourseIdAsync(long courseId)
        {
            return await _context.CourseImageUrls.Where(x => x.CourseId == courseId).ToListAsync();
        }
    }
}

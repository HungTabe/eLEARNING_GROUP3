using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class CourseCategoriesRepository : BaseRepository<CourseCategory>, ICourseCategoriesRepository
    {
        public CourseCategoriesRepository(OnlLearnDBContext context) : base(context)
        {
        }

        public async Task<List<Category>> GetCategoriesByCourseIdAsync(long courseId)
        {
            return await _context.CourseCategories
           .Where(cc => cc.CourseId == courseId)
           .Include(cc => cc.Category)  
           .Select(cc => cc.Category)
           .ToListAsync();
        }

     
        public async Task<long?> GetCategoryIdByCourseIdAsync(long courseId)
        {
            // Trả về CategoryId duy nhất cho một CourseId, nếu không có trả về null
            return await _context.CourseCategories
                .Where(cc => cc.CourseId == courseId)
                .Select(cc =>cc.CategoryId)
                .FirstOrDefaultAsync();
        }
     
        public async Task<List<CourseCategory>> GetCourseCategoriesByCourseIdAsync(long courseId)
        {
            return await _context.CourseCategories
                .Where(cc => cc.CourseId == courseId)
                .ToListAsync();
        }
    }
}

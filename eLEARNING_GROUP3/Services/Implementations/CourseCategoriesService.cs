using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.CourseModels.CategoryModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class CourseCategoriesService : ICourseCategoriesService
    {
        private readonly ICourseCategoriesRepository _courseCategoriesRepository;

        public CourseCategoriesService(ICourseCategoriesRepository courseCategoriesRepository)
        {
            _courseCategoriesRepository = courseCategoriesRepository;
        }

        public async Task<CourseCategory> AddCourseCategoryAsync(CourseCategory courseCategory)
        {
            return await _courseCategoriesRepository.AddAsync(courseCategory);
        }

        public async Task DeleteCourseCategoryAsync(CourseCategory courseCategory)
        {
            await _courseCategoriesRepository.DeleteAsync(courseCategory);
        }

        public async Task<CourseCategory> GetByCourseIdAsync(long courseId, long categoryId)
        {
            return await _courseCategoriesRepository.GetByIdAsync(courseId,categoryId);

        }

        public async Task<List<Category>> GetCategoriesByCourseIdAsync(long courseId)
        {
            return await _courseCategoriesRepository.GetCategoriesByCourseIdAsync(courseId);
        }

        public async Task<long?> GetCategoryIdsByCourseIdAsync(long courseId)
        {
            return await _courseCategoriesRepository.GetCategoryIdByCourseIdAsync(courseId);
        }



        // Lấy danh sách CourseCategory liên kết với CourseId
        public async Task<List<CourseCategory>> GetCourseCategoriesByCourseIdAsync(long courseId)
        {
            return await _courseCategoriesRepository.GetCourseCategoriesByCourseIdAsync(courseId);
        }

        public async Task UpdateCourseCategoriesAsync(CourseCategory courseCategory)
        {
             await _courseCategoriesRepository.UpdateAsync(courseCategory);
        }
    }
}

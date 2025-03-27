using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class CourseImageService : ICourseImageService
    {
        private readonly ICourseImageRepository _courseImageRepository;

        public CourseImageService(ICourseImageRepository courseImageRepository)
        {
            _courseImageRepository = courseImageRepository;
        }

        public async Task<CourseImageUrl> AddCourseImageUrlAsync(CourseImageUrl courseImageUrl)
        {
            return await _courseImageRepository.AddAsync(courseImageUrl);
        }

        public async Task<CourseImageUrl> GetByCourseIdAsync(long courseId)
        {
            return await _courseImageRepository.GetByCourseIdAsync(courseId);
        }

        public async Task<List<CourseImageUrl>> GetListImgByCourseIdAsync(long courseId)
        {
            return await _courseImageRepository.GetListImgByCourseIdAsync(courseId);
        }

        public async Task UpdateCourseImageUrAsync(CourseImageUrl courseImageUrl)
        {
             await _courseImageRepository.UpdateAsync(courseImageUrl);
        }
    }
}

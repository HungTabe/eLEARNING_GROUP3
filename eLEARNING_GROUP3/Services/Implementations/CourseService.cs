using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
	public class CourseService : ICourseService
	{
		private readonly ICourseRepository _courseRepository;

		public CourseService(ICourseRepository courseRepository)
		{
			_courseRepository = courseRepository;
		}

		public async Task<Course> AddCourseAsync(Course course)
		{
			return await _courseRepository.AddAsync(course);
		}

		public async Task<IEnumerable<Course>> GetAllCourseAsync()
		{
			return await _courseRepository.GetAllCourseAsync();
		}
        public Task<Course?> AdminReviewGetCourseByIdAsync(long courseId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<Course>> GetAllCourseAsync()
        //{
        //    return await _courseRepository.GetAllCourseAsync();
        //}

        public async Task<IEnumerable<Course>> GetAllCourseByStatusAsync()
        {
            return await _courseRepository.GetAllCourseByStatusAsync();
        }

        public async Task<Course?> GetCourseByIdAsync(long courseId)
		{
			return await _courseRepository.GetByIdAsync(courseId);
		}

		public async Task<Course> GetInforCourseByIdAsync(long courseId)
		{
			return await _courseRepository.GetInforCourseByIdAsync(courseId);
		}
        public async Task<IEnumerable<Admin_ReviewCourseDto>> GetCoursesListNotApproveYetAsync()
        {
           return await _courseRepository.GetCoursesListNotApproveYetAsync();
        }

        //public async Task<Course> GetInforCourseByIdAsync(long courseId)
        //{
        //    return await _courseRepository.GetInforCourseByIdAsync(courseId);
        //}

		public async Task UpdateCourseAsync(Course course)
		{
			await _courseRepository.UpdateAsync(course);
		}

		public async Task<CourseDetailDTO> GetCourseDetailAsync(long courseId)
		{
			return await _courseRepository.GetCourseDetailAsync(courseId);
		}

        public async Task<bool> CheckPurchaseCourseAsync(long? userId, long couseId)
        {
            return await _courseRepository.CheckUserPurchaseCourseAsync(userId, couseId);
        }
    }

}

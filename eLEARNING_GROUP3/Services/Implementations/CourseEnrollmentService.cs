using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class CourseEnrollmentService : ICourseEnrollmentService
    {
        private readonly ICourseEnrollentRepository _courseEnrollentRepository;
        public CourseEnrollmentService(ICourseEnrollentRepository courseEnrollentRepository )
        {
            _courseEnrollentRepository = courseEnrollentRepository;
        }
        public async Task<bool> AddCourseEnrollmmentAsync(CourseEnrollment courseEnrollment)
        {
            var enrollment = await _courseEnrollentRepository.AddAsync(courseEnrollment);
            return courseEnrollment != null;
        }
    }
}

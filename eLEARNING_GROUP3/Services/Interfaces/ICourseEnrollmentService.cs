using OnlineLearning.Models.Domains.UserCourseRelationship;

namespace OnlineLearning.Services.Interfaces
{
    public interface ICourseEnrollmentService
    {
        Task<bool> AddCourseEnrollmmentAsync(CourseEnrollment courseEnrollment);
    }
}

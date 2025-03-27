using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Course> AddCourseAsync(Course course);
        Task<IEnumerable<Course>> GetAllCourseAsync();
        Task<IEnumerable<Admin_ReviewCourseDto>> GetCoursesListNotApproveYetAsync();

        Task<IEnumerable<Course>> GetAllCourseByStatusAsync();


        Task<Course?> GetCourseByIdAsync(long courseId);
        Task<Course?> AdminReviewGetCourseByIdAsync(long courseId);

        Task UpdateCourseAsync(Course course);

        Task<Course> GetInforCourseByIdAsync(long courseId);

        // GET COURSE DETAIL (USER)
        Task<CourseDetailDTO> GetCourseDetailAsync(long courseId);

        // CHECK MENTEE COURSE PURCHASED
        Task<bool> CheckPurchaseCourseAsync(long? userId, long couseId);
    }

}

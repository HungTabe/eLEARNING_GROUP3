using OnlineLearning.Data;
using OnlineLearning.Models.Domains.UserCourseRelationship;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class CourseEnrollmentRepository : BaseRepository<CourseEnrollment>, ICourseEnrollentRepository
    {
        public CourseEnrollmentRepository(OnlLearnDBContext dBContext) : base(dBContext) { }
	}
}

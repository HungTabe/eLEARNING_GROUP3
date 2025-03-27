using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.Domains.UserCourseRelationship;

namespace OnlineLearning.Models.DTOs
{
	public class CourseDetailDTO
	{
		public Course Course { get; set; }
		public double AvgRating { get; set; }
		public int AmountRating { get; set; }
		public int EnrollmentCount { get; set; }
		public int Lecture { get; set; }
		public List<CoursePreviewDTO> RelatedCourses { get; set; } = new List<CoursePreviewDTO>();
        public List<CourseRating> courseRatings { get; set; }
		public List<Module> Modules { get; set; } = new List<Module>();
    }

}

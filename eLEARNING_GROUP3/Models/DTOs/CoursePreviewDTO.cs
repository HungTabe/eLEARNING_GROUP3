using OnlineLearning.Models.Domains.UserCourseRelationship;

namespace OnlineLearning.Models.DTOs
{
	public class CoursePreviewDTO
	{
		public long CourseId { get; set; }
		public string CourseName { get; set; } = string.Empty;
		public string? Image { get; set; }
		public decimal Price { get; set; }
		public decimal AvgRating { get; set; }
		public int SumOfRating { get; set; }
	}
}

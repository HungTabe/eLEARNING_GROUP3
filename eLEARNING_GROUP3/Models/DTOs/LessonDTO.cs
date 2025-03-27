using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
	public class LessonDTO
	{
		[Required(ErrorMessage = "LessonName không được để trống!")]

		public string LessonName { get; set; } = null!;

		[Required(ErrorMessage = "LessonContent không được để trống!")]

		public string? LessonContent { get; set; }

		[Required(ErrorMessage = "LessonVideo không được để trống!")]

		[RegularExpression(@"^https:\/\/www\.youtube\.com\/watch\?v=[a-zA-Z0-9_-]{11}$",
		ErrorMessage = "Vui lòng nhập đường dẫn YouTube hợp lệ dạng: https://www.youtube.com/watch?v=VIDEO_ID")]
		public string? LessonVideo { get; set; }

		public long ModuleId { get; set; }

		public int LessonNumber { get; set; }

		public string ModuleName { get; set; }
		public int ModuleNumber { get; set; }

		public long CourseId { get; set; }


	}

}

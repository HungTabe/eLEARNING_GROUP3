using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
	public class LessonService : ILessonService
	{
		private readonly ILessonRepository _lessonRepository;

		public LessonService(ILessonRepository lessonRepository)
		{
			_lessonRepository = lessonRepository;
		}

		public async Task<Lesson> AddLessonAsync(Lesson lesson)
		{
			return await _lessonRepository.AddAsync(lesson);
		}

		public async Task<IEnumerable<Lesson>> GetAllLessonByModuleIdAsync(long moduleId)
		{
			return await _lessonRepository.GetAllLessonByModuleIdAsync(moduleId);
		}

		public async Task<int> GetNextLessonNumberAsync(long moduleId)
		{
			return await _lessonRepository.GetNextLessonNumberAsync(moduleId);
		}

		public async Task<Lesson> GetLessonByIdAsync(int lessonId)
		{
			return await _lessonRepository.GetByIdAsync(lessonId);
		}

		public async Task<bool> UpdateLessonAsync(Lesson lesson)
		{
			if (lesson == null) return false;
			try
			{
				await _lessonRepository.UpdateAsync(lesson);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		//// Hải
		//// Feature:manage lesson
		//public async Task<bool> AddNewLessonAsync(LessonDTO lessonDTO)
		//{
		//	var lesson = new Lesson
		//	{
		//		LessonName = lessonDTO.LessonName,
		//		LessonContent = lessonDTO.LessonContent,
		//		LessonNumber = lessonDTO.LessonNumber,
		//		LessonVideo = ConvertToYouTubeEmbed(lessonDTO.LessonVideo),
		//		CreatedAt = DateTime.UtcNow,
		//		ModuleId = lessonDTO.ModuleId,
		//		Status = Enums.CommonStatus.Showed
		//	};

		//	await _lessonRepository.AddAsync(lesson);
		//	return true;
		//}

		//// Func: convert normal link -> embeded link
		//public static string ConvertToYouTubeEmbed(string youtubeUrl)
		//{
		//	if (string.IsNullOrWhiteSpace(youtubeUrl))
		//		return string.Empty;

		//	var regex = new System.Text.RegularExpressions.Regex(@"(?:youtu\.be/|youtube\.com/(?:embed/|v/|watch\?v=|.*[?&]v=))([^?&]+)");
		//	var match = regex.Match(youtubeUrl);

		//	if (match.Success)
		//	{
		//		string videoId = match.Groups[1].Value;
		//		return $"https://www.youtube.com/embed/{videoId}?rel=0"; // ?rel=0 để không hiển thị video liên quan
		//	}

		//	return string.Empty; // Trả về chuỗi rỗng nếu URL không hợp lệ
		//}
	}
}

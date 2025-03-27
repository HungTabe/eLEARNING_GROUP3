using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Services.Interfaces
{
	public interface ILessonService
	{
		Task<Lesson> AddLessonAsync(Lesson lesson);

		Task<IEnumerable<Lesson>> GetAllLessonByModuleIdAsync(long moduleId);
		Task<int> GetNextLessonNumberAsync(long moduleId);
		Task<Lesson> GetLessonByIdAsync(int lessonId);
		Task<bool> UpdateLessonAsync(Lesson lesson);
		// Hải
		// Feature:manage lesson
		//Task<bool> AddNewLessonAsync(LessonDTO lessonDTO);

	}
}

using OnlineLearning.Models.Domains.CourseModels.LessonModels;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
    public class ModuleDto
    {
        [Required(ErrorMessage = "Module Name is required")]

        [MaxLength(50, ErrorMessage = "ModuleName không được vượt quá 50 ký tự!")]

        public string ModuleName { get; set; }
        public int ModuleNumber { get; set; }

        public long CourseId { get; set; }

        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

    }
}

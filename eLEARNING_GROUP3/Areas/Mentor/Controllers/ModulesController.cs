using Humanizer;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.Domains.CourseModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    public class ModulesController : Controller
    {

        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public async Task<IActionResult> Index(long courseId)
        {
            int nextModuleNumber = await _moduleService.GetNextModuleNumberAsync(courseId);

            var model = new ModuleDto
            {
                CourseId = courseId,
                ModuleNumber = nextModuleNumber
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddModules(ModuleDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Trả về view nếu validation không hợp lệ
            }
            if (model.ModuleName.Length > 255)
            {
                ModelState.AddModelError("ModuleName", "ModuleName không được vượt quá 255 ký tự!");
            }

            var module = new Module
            {
                CourseId = model.CourseId,
                ModuleName = model.ModuleName,
                ModuleNumber = model.ModuleNumber
            };

            await _moduleService.AddModuleAsync(module);

            return RedirectToAction("CourseEdit", "Course", new { id = model.CourseId });
        }



    }
}

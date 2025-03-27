using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.AIModels;
using OnlineLearning.Services.Interfaces.AI;

namespace OnlineLearning.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(RoleType.ADMIN))]
    [Area("Admin")]
    public class AITrainingController : Controller
    {
        private readonly IAITrainingService _aiTrainingService;

        public AITrainingController(IAITrainingService aiTrainingService)
        {
            _aiTrainingService = aiTrainingService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingData(AITrainingData model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            await _aiTrainingService.AddTrainingDataWithEmbeddingAsync(model.InputText, model.ExpectedOutput);
            ViewBag.Message = "Dữ liệu huấn luyện đã được thêm thành công!";
            return View("Index");
        }
    }

}

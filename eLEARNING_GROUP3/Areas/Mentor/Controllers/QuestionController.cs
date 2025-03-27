using Microsoft.AspNetCore.Mvc;

namespace OnlineLearning.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    public class QuestionController : Controller
    {
        public IActionResult QuestionsView()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace OnlineLearning.Controllers
{
    public class TestAPIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

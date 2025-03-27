using Microsoft.AspNetCore.Mvc;

namespace OnlineLearning.Controllers
{
    public class ErrorController : Controller
    {
        public  IActionResult AccessDenied()
        {
            return View();
        }
    }
}
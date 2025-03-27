using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.AI;

namespace OnlineLearning.Controllers
{
    public class ChatbotController : Controller
    {
        private readonly IChatbotService _chatbotService;
        private readonly IUserService _userService;

        public ChatbotController(IChatbotService chatbotService, IUserService userService)
        {
            _chatbotService = chatbotService;
            _userService = userService;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var userIdString = HttpContext.Session.GetString("UserId");

        //    // Chưa đăng nhập
        //    if (string.IsNullOrEmpty(userIdString))
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }

        //    // Lỗi xảy ra 
        //    if (!long.TryParse(userIdString, out long userId))
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    var user = await _userService.GetUserByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    var historyMessage = await _chatbotService.GetHistoryChatAsyncByUserId(userId);

        //    return View(historyMessage);
        //}
    }
}

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.ViewModels;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.AI;
using static System.Net.WebRequestMethods;

namespace OnlineLearning.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IChatbotService _chatbotService;

    public HomeController(ILogger<HomeController> logger,IUserService userService, IChatbotService chatbotService)
    {
        _logger = logger;
        _userService = userService;
        _chatbotService = chatbotService;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetString("UserId");
       
        var viewModel = new HomeViewModel();
        if (!string.IsNullOrEmpty(userId))
        {
            var headerPartial = await _userService.GetUserHeaderAsync(long.Parse(userId));
            HttpContext.Session.SetString("HeaderPartial", headerPartial);
            // Get chat history
            viewModel.ChatbotMessage = await _chatbotService.GetHistoryChatAsyncByUserId(long.Parse(userId));
            viewModel.UserId = long.Parse(userId);
        }
        return View(viewModel);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

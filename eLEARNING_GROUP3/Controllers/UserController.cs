using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }
        public async Task<IActionResult> Details(long id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }
    }
}

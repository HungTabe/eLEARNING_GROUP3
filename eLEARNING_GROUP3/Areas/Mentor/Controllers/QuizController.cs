using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Services.Implementations;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Areas.Mentor.Controllers
{
    [Area("Mentor")]
    public class QuizController : Controller
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // Hiển thị form tạo quiz
       
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> CreateQuiz(QuizDTO quizDTO)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",quizDTO);
            }

            await _quizService.CreateQuizAsync(quizDTO);
            return RedirectToAction("Index", "Home");
            // Điều hướng về trang danh sách Quiz
        }
    }
}

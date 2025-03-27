using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Controllers
{
    public class FAQsController : Controller
    {
        private readonly IFAQsService _faqsService;
        public FAQsController(IFAQsService faqsService)
        {
            _faqsService = faqsService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await _faqsService.GetListFAQsToView();
            return View(faqs);
        }
    }
}

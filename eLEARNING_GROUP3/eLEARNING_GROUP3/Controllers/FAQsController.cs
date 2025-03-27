using Microsoft.AspNetCore.Mvc;
namespace eLEARNING_GROUP3.Controllers


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

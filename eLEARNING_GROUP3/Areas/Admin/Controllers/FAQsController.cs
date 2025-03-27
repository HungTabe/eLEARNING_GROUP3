using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Areas.Admin.Controllers
{
    [Authorize(Roles = nameof(RoleType.ADMIN))]
    [Area("Admin")]
    public class FAQsController : Controller
    {
        private readonly IFAQsService _faqsService;
        public FAQsController(IFAQsService faqsService)
        {
            _faqsService = faqsService;
        }

        // GET: FAQs
        public async Task<IActionResult> Index()
        {
            return View(await _faqsService.GetAllAsync());
        }

        // GET: FAQs/Details/5
        public async Task<IActionResult> Details(long id)
        {
            var fAQ = await _faqsService.GetByIdAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // GET: FAQs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FAQs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaqId,Question,Answer,CommonStatus")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                await _faqsService.AddAsync(fAQ);
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: FAQs/Edit/5
        public async Task<IActionResult> Edit(long id)
        {
            var fAQ = await _faqsService.GetByIdAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("FaqId,Question,Answer,CreatedAt,UpdatedAt,CommonStatus")] FAQ fAQ)
        {
            if (id != fAQ.FaqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _faqsService.UpdateAsync(fAQ);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FAQExists(fAQ.FaqId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: FAQs/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            var fAQ = await _faqsService.GetByIdAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: FAQs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var fAQ = await _faqsService.GetByIdAsync(id);
            if (fAQ != null)
            {
                await _faqsService.DeleteAsync(fAQ);
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FAQExists(long id)
        {
            //return _context.FAQs.Any(e => e.FaqId == id);
            return await _faqsService.IsFAQExists(id);
        }
    }
}

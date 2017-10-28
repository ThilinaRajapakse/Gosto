using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gosto.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Gosto.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PromotionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public PromotionsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Promotions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Promotion.ToListAsync());
        }

        // GET: Promotions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotion.SingleOrDefaultAsync(m => m.ID == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // GET: Promotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Name")] Promotion promotion, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                        promotion.ImagePath = Path.Combine(uploads, file.FileName);
                    }
                }
                _context.Add(promotion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotion.SingleOrDefaultAsync(m => m.ID == id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Name")] Promotion promotion)
        {
            if (id != promotion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(promotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromotionExists(promotion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotion.SingleOrDefaultAsync(m => m.ID == id);
            if (promotion == null)
            {
                return NotFound();
            }

            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promotion = await _context.Promotion.SingleOrDefaultAsync(m => m.ID == id);
            if(System.IO.File.Exists(promotion.ImagePath))
            {
                System.IO.File.Delete(promotion.ImagePath);
            }
            _context.Promotion.Remove(promotion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PromotionExists(int id)
        {
            return _context.Promotion.Any(e => e.ID == id);
        }
    }
}

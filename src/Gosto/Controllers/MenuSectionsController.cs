using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gosto.Data;
using Gosto.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gosto.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class MenuSectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuSectionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: MenuSections
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuSections.ToListAsync());
        }

        // GET: MenuSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuSection = await _context.MenuSections
                .Include(m => m.MenuItems)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);
            if (menuSection == null)
            {
                return NotFound();
            }

            return View(menuSection);
        }

        // GET: MenuSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuSections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,Name")] MenuSection menuSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuSection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuSection);
        }

        // GET: MenuSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuSection = await _context.MenuSections.SingleOrDefaultAsync(m => m.ID == id);
            if (menuSection == null)
            {
                return NotFound();
            }
            return View(menuSection);
        }

        // POST: MenuSections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,Name")] MenuSection menuSection)
        {
            if (id != menuSection.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuSectionExists(menuSection.ID))
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
            return View(menuSection);
        }

        // GET: MenuSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuSection = await _context.MenuSections.SingleOrDefaultAsync(m => m.ID == id);
            if (menuSection == null)
            {
                return NotFound();
            }

            return View(menuSection);
        }

        // POST: MenuSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuSection = await _context.MenuSections.SingleOrDefaultAsync(m => m.ID == id);
            _context.MenuSections.Remove(menuSection);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuSectionExists(int id)
        {
            return _context.MenuSections.Any(e => e.ID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gosto.Data;
using Gosto.Models;

namespace Gosto.Controllers
{
    public class MenuItemsController : Controller
    {
        private readonly MenuContext _context;

        public MenuItemsController(MenuContext context)
        {
            _context = context;    
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var menuContext = _context.MenuItems.Include(m => m.MenuSection);
            return View(await menuContext.ToListAsync());
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // GET: MenuItems/Create
        public IActionResult Create()
        {
            ViewData["MenuSectionID"] = new SelectList(_context.MenuSections, "ID", "Name");
            return View();
        }

        // POST: MenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,MenuSectionID,Name,Price")] MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MenuSectionID"] = new SelectList(_context.MenuSections, "ID", "Name", menuItem.MenuSectionID);
            return View(menuItem);
        }

        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (menuItem == null)
            {
                return NotFound();
            }
            ViewData["MenuSectionID"] = new SelectList(_context.MenuSections, "ID", "Name", menuItem.MenuSectionID);
            return View(menuItem);
        }

        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Description,MenuSectionID,Name,Price")] MenuItem menuItem)
        {
            if (id != menuItem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemExists(menuItem.ID))
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
            ViewData["MenuSectionID"] = new SelectList(_context.MenuSections, "ID", "Name", menuItem.MenuSectionID);
            return View(menuItem);
        }

        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            return View(menuItem);
        }

        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.ID == id);
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.ID == id);
        }
    }
}

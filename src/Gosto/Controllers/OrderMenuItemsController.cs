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
    public class OrderMenuItemsController : Controller
    {
        private readonly MenuContext _context;

        public OrderMenuItemsController(MenuContext context)
        {
            _context = context;    
        }

        // GET: OrderMenuItems
        public async Task<IActionResult> Index()
        {
            var menuContext = _context.OrderMenuItems.Include(o => o.MenuItem);
            return View(await menuContext.ToListAsync());
        }

        // GET: OrderMenuItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItems = await _context.OrderMenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (orderMenuItems == null)
            {
                return NotFound();
            }

            return View(orderMenuItems);
        }

        // GET: OrderMenuItems/Create
        public IActionResult Create()
        {
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "ID", "Name");
            return View();
        }

        // POST: OrderMenuItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Comments,MenuItemID,Quantity")] OrderMenuItems orderMenuItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderMenuItems);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "ID", "Name", orderMenuItems.MenuItemID);
            return View(orderMenuItems);
        }

        // GET: OrderMenuItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItems = await _context.OrderMenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (orderMenuItems == null)
            {
                return NotFound();
            }
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "ID", "Name", orderMenuItems.MenuItemID);
            return View(orderMenuItems);
        }

        // POST: OrderMenuItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Comments,MenuItemID,Quantity")] OrderMenuItems orderMenuItems)
        {
            if (id != orderMenuItems.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderMenuItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderMenuItemsExists(orderMenuItems.ID))
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
            ViewData["MenuItemID"] = new SelectList(_context.MenuItems, "ID", "Name", orderMenuItems.MenuItemID);
            return View(orderMenuItems);
        }

        // GET: OrderMenuItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderMenuItems = await _context.OrderMenuItems.SingleOrDefaultAsync(m => m.ID == id);
            if (orderMenuItems == null)
            {
                return NotFound();
            }

            return View(orderMenuItems);
        }

        // POST: OrderMenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderMenuItems = await _context.OrderMenuItems.SingleOrDefaultAsync(m => m.ID == id);
            _context.OrderMenuItems.Remove(orderMenuItems);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool OrderMenuItemsExists(int id)
        {
            return _context.OrderMenuItems.Any(e => e.ID == id);
        }
    }
}

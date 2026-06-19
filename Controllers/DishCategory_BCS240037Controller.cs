using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Midterm_BCS240037.Data;
using Midterm_BCS240037.Models;

namespace Midterm_BCS240037.Controllers
{
    public class DishCategory_BCS240037Controller : Controller
    {
        private readonly AppDbContext _context;

        public DishCategory_BCS240037Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: DishCategory_BCS240037
        public async Task<IActionResult> Index()
        {
            return View(await _context.DishCategories.ToListAsync());
        }

        // GET: DishCategory_BCS240037/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory_BCS240037 = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategory_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dishCategory_BCS240037);
        }

        // GET: DishCategory_BCS240037/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishCategory_BCS240037/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] DishCategory_BCS240037 dishCategory_BCS240037)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishCategory_BCS240037);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishCategory_BCS240037);
        }

        // GET: DishCategory_BCS240037/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory_BCS240037 = await _context.DishCategories.FindAsync(id);
            if (dishCategory_BCS240037 == null)
            {
                return NotFound();
            }
            return View(dishCategory_BCS240037);
        }

        // POST: DishCategory_BCS240037/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] DishCategory_BCS240037 dishCategory_BCS240037)
        {
            if (id != dishCategory_BCS240037.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishCategory_BCS240037);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishCategory_BCS240037Exists(dishCategory_BCS240037.Id))
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
            return View(dishCategory_BCS240037);
        }

        // GET: DishCategory_BCS240037/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishCategory_BCS240037 = await _context.DishCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishCategory_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dishCategory_BCS240037);
        }

        // POST: DishCategory_BCS240037/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishCategory_BCS240037 = await _context.DishCategories.FindAsync(id);
            if (dishCategory_BCS240037 != null)
            {
                _context.DishCategories.Remove(dishCategory_BCS240037);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishCategory_BCS240037Exists(int id)
        {
            return _context.DishCategories.Any(e => e.Id == id);
        }
    }
}

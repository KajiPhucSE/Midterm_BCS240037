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
    public class DishImage_BCS240037Controller : Controller
    {
        private readonly AppDbContext _context;

        public DishImage_BCS240037Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: DishImage_BCS240037
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DishImages.Include(d => d.Dish);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DishImage_BCS240037/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishImage_BCS240037 = await _context.DishImages
                .Include(d => d.Dish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishImage_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dishImage_BCS240037);
        }

        // GET: DishImage_BCS240037/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name");
            return View();
        }

        // POST: DishImage_BCS240037/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageUrl,IsThumbnail,DishId")] DishImage_BCS240037 dishImage)
        {
            // Dòng phép thuật để fix lỗi không lưu được ảnh
            ModelState.Remove("Dish");

            if (ModelState.IsValid)
            {
                if (dishImage.IsThumbnail)
                {
                    var oldThumbnails = _context.DishImages.Where(i => i.DishId == dishImage.DishId && i.IsThumbnail);
                    foreach (var img in oldThumbnails)
                    {
                        img.IsThumbnail = false;
                    }
                }

                _context.Add(dishImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Lưu thành công sẽ tự động quay về trang Danh sách
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishImage.DishId);
            return View(dishImage);
        }

        // GET: DishImage_BCS240037/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishImage_BCS240037 = await _context.DishImages.FindAsync(id);
            if (dishImage_BCS240037 == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishImage_BCS240037.DishId);
            return View(dishImage_BCS240037);
        }

        // POST: DishImage_BCS240037/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageUrl,IsThumbnail,DishId")] DishImage_BCS240037 dishImage)
        {
            if (id != dishImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // NGHIỆP VỤ: Đổi ảnh đại diện (thu hồi quyền của các ảnh khác cùng món ăn) [cite: 64, 65]
                    if (dishImage.IsThumbnail)
                    {
                        var oldThumbnails = await _context.DishImages
                            .Where(i => i.DishId == dishImage.DishId && i.Id != dishImage.Id && i.IsThumbnail)
                            .ToListAsync();
                        foreach (var img in oldThumbnails)
                        {
                            img.IsThumbnail = false;
                        }
                    }

                    _context.Update(dishImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishImage_BCS240037Exists(dishImage.Id)) // Chú ý tên hàm này phải khớp với code gốc sinh ra ở dưới cùng
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
            ViewData["DishId"] = new SelectList(_context.Dishes, "Id", "Name", dishImage.DishId);
            return View(dishImage);
        }

        // GET: DishImage_BCS240037/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishImage_BCS240037 = await _context.DishImages
                .Include(d => d.Dish)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dishImage_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dishImage_BCS240037);
        }

        // POST: DishImage_BCS240037/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishImage_BCS240037 = await _context.DishImages.FindAsync(id);
            if (dishImage_BCS240037 != null)
            {
                _context.DishImages.Remove(dishImage_BCS240037);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishImage_BCS240037Exists(int id)
        {
            return _context.DishImages.Any(e => e.Id == id);
        }
    }
}

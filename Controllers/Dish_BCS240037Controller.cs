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
    public class Dish_BCS240037Controller : Controller
    {
        private readonly AppDbContext _context;

        public Dish_BCS240037Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: Dish_BCS240037
        public async Task<IActionResult> Index(string searchString, int? categoryId, bool? isAvailable, decimal? minPrice, decimal? maxPrice, string sortOrder)
        {
            // Xử lý lỗi: Khoảng giá không hợp lệ 
            if (minPrice.HasValue && maxPrice.HasValue && minPrice > maxPrice)
            {
                ViewBag.ErrorMessage = "Lỗi: Khoảng giá không hợp lệ (Giá từ không được lớn hơn Đến giá)!";

                // Phải load lại danh sách Category để form View không bị lỗi dropdown
                ViewBag.CategoryId = new SelectList(_context.DishCategories, "Id", "Name", categoryId);

                return View(new List<Dish_BCS240037>()); // Ép trả về danh sách rỗng
            }
            // Bắt đầu truy vấn (không load hết lên RAM để tối ưu)
            var query = _context.Dishes
                .Include(d => d.DishCategory)
                .AsQueryable();

            // 1. Tìm kiếm theo tên
            if (!string.IsNullOrEmpty(searchString))
                query = query.Where(d => d.Name.Contains(searchString));

            // 2. Lọc theo loại món ăn
            if (categoryId.HasValue)
                query = query.Where(d => d.DishCategoryId == categoryId.Value);

            // 3. Lọc theo trạng thái đang bán
            if (isAvailable.HasValue)
                query = query.Where(d => d.IsAvailable == isAvailable.Value);

            // 4. Lọc theo khoảng giá
            if (minPrice.HasValue && minPrice >= 0)
                query = query.Where(d => d.Price >= minPrice.Value);
            if (maxPrice.HasValue && maxPrice >= minPrice)
                query = query.Where(d => d.Price <= maxPrice.Value);

            // 5. Sắp xếp
            query = sortOrder switch
            {
                "price_desc" => query.OrderByDescending(d => d.Price),
                "price_asc" => query.OrderBy(d => d.Price),
                "time_asc" => query.OrderBy(d => d.PreparationTime),
                _ => query.OrderByDescending(d => d.Id) // Mặc định hiển thị món mới nhất
            };

            // 6. Giữ lại các điều kiện tìm kiếm trên form sau khi bấm Submit
            ViewBag.SearchString = searchString;
            ViewBag.CategoryId = new SelectList(_context.DishCategories, "Id", "Name", categoryId);
            ViewBag.IsAvailable = isAvailable;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.SortOrder = sortOrder;

            return View(await query.ToListAsync());
        }

        // GET: Dish_BCS240037/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish_BCS240037 = await _context.Dishes
                .Include(d => d.DishCategory)
                .Include(d => d.DishImages)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dish_BCS240037);
        }

        // GET: Dish_BCS240037/Create
        public IActionResult Create()
        {
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name");
            return View();
        }

        // POST: Dish_BCS240037/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,PreparationTime,IsAvailable,Description,DishCategoryId")] Dish_BCS240037 dish_BCS240037)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish_BCS240037);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", dish_BCS240037.DishCategoryId);
            return View(dish_BCS240037);
        }

        // GET: Dish_BCS240037/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish_BCS240037 = await _context.Dishes.FindAsync(id);
            if (dish_BCS240037 == null)
            {
                return NotFound();
            }
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", dish_BCS240037.DishCategoryId);
            return View(dish_BCS240037);
        }

        // POST: Dish_BCS240037/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,PreparationTime,IsAvailable,Description,DishCategoryId")] Dish_BCS240037 dish_BCS240037)
        {
            if (id != dish_BCS240037.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish_BCS240037);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Dish_BCS240037Exists(dish_BCS240037.Id))
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
            ViewData["DishCategoryId"] = new SelectList(_context.DishCategories, "Id", "Name", dish_BCS240037.DishCategoryId);
            return View(dish_BCS240037);
        }

        // GET: Dish_BCS240037/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish_BCS240037 = await _context.Dishes
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dish_BCS240037 == null)
            {
                return NotFound();
            }

            return View(dish_BCS240037);
        }

        // POST: Dish_BCS240037/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish_BCS240037 = await _context.Dishes.FindAsync(id);
            if (dish_BCS240037 != null)
            {
                _context.Dishes.Remove(dish_BCS240037);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Dish_BCS240037Exists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }
}

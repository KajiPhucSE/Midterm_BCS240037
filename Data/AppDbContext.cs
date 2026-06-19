using Microsoft.EntityFrameworkCore;
using Midterm_BCS240037.Models;

namespace Midterm_BCS240037.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Khai báo 3 bảng sẽ được tạo dưới SQL
        public DbSet<DishCategory_BCS240037> DishCategories { get; set; }
        public DbSet<Dish_BCS240037> Dishes { get; set; }
        public DbSet<DishImage_BCS240037> DishImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // RÀNG BUỘC 1: Tên món ăn không được trùng trong cùng một loại (Yêu cầu đề bài)
            modelBuilder.Entity<Dish_BCS240037>()
                .HasIndex(d => new { d.Name, d.DishCategoryId })
                .IsUnique();

            // RÀNG BUỘC 2: Không cho phép xóa loại món ăn nếu đang có món ăn sử dụng (Yêu cầu đề bài)
            modelBuilder.Entity<Dish_BCS240037>()
                .HasOne(d => d.DishCategory)
                .WithMany(c => c.Dishes)
                .HasForeignKey(d => d.DishCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
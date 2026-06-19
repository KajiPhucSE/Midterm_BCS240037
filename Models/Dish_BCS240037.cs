using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Midterm_BCS240037.Models
{
    [Table("Dishes_BCS240037")]
    public class Dish_BCS240037
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên món ăn không được để trống")]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Giá tiền phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Thời gian chế biến phải lớn hơn 0 phút")]
        public int PreparationTime { get; set; }

        public bool IsAvailable { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Phải chọn loại món ăn")]
        public int DishCategoryId { get; set; }

        [ForeignKey("DishCategoryId")]
        public DishCategory_BCS240037 DishCategory { get; set; }

        public ICollection<DishImage_BCS240037> DishImages { get; set; }

        [NotMapped]
        public string PrepTimeCategory => PreparationTime <= 15 ? "Nhanh" : "Thông thường";
    }
}
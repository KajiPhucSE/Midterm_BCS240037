using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Midterm_BCS240037.Models
{
    [Table("DishCategories_BCS240037")]
    public class DishCategory_BCS240037
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên loại món ăn không được để trống")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Dish_BCS240037> Dishes { get; set; }
    }
}
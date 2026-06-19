using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Midterm_BCS240037.Models
{
    [Table("DishImages_BCS240037")]
    public class DishImage_BCS240037
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Đường dẫn ảnh (URL) không được trống")]
        public string ImageUrl { get; set; }

        public bool IsThumbnail { get; set; }

        public int DishId { get; set; }

        [ForeignKey("DishId")]
        public Dish_BCS240037 Dish { get; set; }
    }
}
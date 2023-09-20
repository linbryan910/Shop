using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Models
{
    public class Item
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string? ImageSource { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required, DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)"), Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage =("Please enter a valid integer number"))]
        public int Amount { get; set; }
    }
}

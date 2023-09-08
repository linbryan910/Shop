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
        public string? Description { get; set; }
        [Required]
        public string Category { get; set; } = string.Empty;
        [Required, DataType(DataType.Currency), Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}

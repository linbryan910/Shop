using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Purchase
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required, Range(0, int.MaxValue)]
        public int Amount { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

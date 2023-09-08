using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class CustomerAccount
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}

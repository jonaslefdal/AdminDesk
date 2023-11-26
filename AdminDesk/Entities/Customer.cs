using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{

    [Table("customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerFirstName { get; set; }
        [Required]
        public string CustomerLastName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerStreet { get; set; }
        [Required]
        public string CustomerCity { get; set; }
        [Required]
        public string CustomerZipcode { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string CustomerComment { get; set; }

    }
}

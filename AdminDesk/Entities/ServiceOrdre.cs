using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{
    [Table("serviceorders")]
    public class ServiceOrder
    {
        [Required]

        public int ServiceOrderId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public virtual Customer Customer { get; set; } // Change the type to Customer
        [Required]
        public string Mechanic { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string FutureMaintenance { get; set; }
        [Required]
        public string CreatedById { get; set; }
        [Required]
        public string OrderStatus { get; set; }
        [Required]
        public string TotalWorkHours { get; set; }
        



    }
}

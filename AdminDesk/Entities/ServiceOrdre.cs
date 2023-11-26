using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{
    [Table("serviceorders")]
    public class ServiceOrder
    {
        public int ServiceOrderId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } // Change the type to Customer
        public string Mechanic { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string FutureMaintenance { get; set; }
        public string CreatedById { get; set; }
        public string OrderStatus { get; set; }
        public string TotalWorkHours { get; set; }
        


    }
}

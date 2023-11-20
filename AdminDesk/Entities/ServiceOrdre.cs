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
        public int CreatedById { get; set; }
        public string OrderStatus { get; set; }
        public bool ReserveDeler { get; set; }
        public string TotalArbeidsTimer { get; set; }

    }

    [Table("customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerComment { get; set; }

        [Table("report")]
        public class Report
        {
            public int ReportId { get; set; }
            public int ServiceOrderId { get; set; }

        }
    }
}

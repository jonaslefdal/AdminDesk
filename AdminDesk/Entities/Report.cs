using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{
    [Table("report")]
    public class Report
    {
        public int ReportId { get; set; }
        public int ServiceOrderId { get; set; }
        
        

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
}

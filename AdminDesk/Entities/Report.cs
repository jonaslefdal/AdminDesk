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
        public string ServiceType { get; set; }
        public string MechanicComment { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime ReportWriteDate { get; set; }
        public string UserSign { get; set; }
        
    }
    
}

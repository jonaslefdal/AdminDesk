using System;
using System.ComponentModel.DataAnnotations;
using AdminDesk.Entities;
using AdminDesk.Models.ServiceOrder;
using static System.Net.Mime.MediaTypeNames;

namespace AdminDesk.Models.Report
{

    public class ReportFullViewModel
    {
        public ReportFullViewModel()
        {


            UpsertModel = new ReportViewModel();

            ReportList = new List<ReportViewModel>();
        }

        public ReportViewModel UpsertModel { get; set; }
        public List<ReportViewModel> ReportList { get; set; }
    }


    public class ReportViewModel
    {
        [Required]
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
}


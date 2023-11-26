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
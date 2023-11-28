using System;//Importerer standard .NET-klasser
using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using AdminDesk.Entities;//Importerer entitetsklasser fra AdminDesk
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen
using static System.Net.Mime.MediaTypeNames;//Importerer for å tilgang til mediatypenavn

namespace AdminDesk.Models.Report
{

    public class ReportFullViewModel
    {
        public ReportFullViewModel() //Oppretter ViewModel for rapporter
        {
            UpsertModel = new ReportViewModel(); //Constructor som initialiserer UpsertModel og ReportList

            ReportList = new List<ReportViewModel>();
        }

        public ReportViewModel UpsertModel { get; set; }
        public List<ReportViewModel> ReportList { get; set; }
    }

    public class ReportViewModel //ViewModel som beskriver attributter for rapporter
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
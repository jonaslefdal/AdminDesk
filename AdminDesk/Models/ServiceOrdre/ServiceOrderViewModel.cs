using System;//Importerer standard .NET-klasser
using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using AdminDesk.Entities;//Importerer entitetsklasser fra AdminDesk
using AdminDesk.Models.Report;//Importerer rapportmodeller fra AdminDesk
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen
using static System.Net.Mime.MediaTypeNames;//Importerer for å tilgang til mediatypenavn

namespace AdminDesk.Models.ServiceOrder
{

    //Kombinerer Serviceordre- og report-viewmodel til et integrert klasse

    public class CompositeViewModel
    {
        public ServiceOrderFullViewModel ServiceOrderModel { get; set; }
        public ReportFullViewModel ReportModel { get; set; }
    }
        //Håndtere visning og oppdatering av serviceordre

    public class ServiceOrderFullViewModel
    {
        //Constructor som initialiserer attributter for oppretting og oppdatering av serviceordre

        public ServiceOrderFullViewModel()
        {


            UpsertModel = new ServiceOrderViewModel();

            ServiceOrderList = new List<ServiceOrderViewModel>();
        }

        public ServiceOrderViewModel UpsertModel { get; set; }
        public List<ServiceOrderViewModel> ServiceOrderList { get; set; }
    }

    //Håndterer data og detaljer om spesifikke serviceordre

    public class ServiceOrderViewModel
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
        public string CreatedById { get; set; }
        public string OrderStatus { get; set; }
        public bool ReserveDeler { get; set; }
        public string TotalWorkHours { get; set; }

    }
    //Håndterer data og detaljer om kunder

    public class CustomerViewModel
    {
        [Required]
        [Key]//Primærnøkkel for identfikasjon
        public int CustomerId { get; set; }
        
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public string CustomerComment { get; set; }
    }
}


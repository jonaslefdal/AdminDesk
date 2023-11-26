using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using System.ComponentModel.DataAnnotations.Schema;//Importerer for å tilkoble modellene til databasetabeller.
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen

namespace AdminDesk.Entities//Definerer navneområde for entitestsklasser
{
    // Definerer Serviceordre-klasse som tilsvarer serviceordre-tabell i database.

    [Table("serviceorders")]
    public class ServiceOrder
    {
        public int ServiceOrderId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Customer Customer { get; set; }
        public string Mechanic { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }
        public string FutureMaintenance { get; set; }
        public int CreatedById { get; set; }
        public string OrderStatus { get; set; }
        public bool ReserveDeler { get; set; }
        public string TotalWorkHours { get; set; }



    }
}
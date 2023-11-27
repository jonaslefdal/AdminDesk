using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using System.ComponentModel.DataAnnotations.Schema;//Importerer for å tilkoble modellene til databasetabeller.
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen

namespace AdminDesk.Entities//Definerer navneområde for entitetsklasser
{
    // Definerer Report-klasse som tilsvarer report-tabell i database.

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
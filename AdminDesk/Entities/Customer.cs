using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using System.ComponentModel.DataAnnotations.Schema;//Importerer for å tilkoble modellene til databasetabeller.
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen

namespace AdminDesk.Entities//Definerer navneområde for entitetsklasser
{
    // Definerer Customer-klasse som tilsvarer customer-tabell i database.
    [Table("customer")]
    public class Customer
    {
        [Key]//Nøkkel for å identefisere Customer-entitet
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
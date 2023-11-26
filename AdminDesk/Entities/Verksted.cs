using System.ComponentModel.DataAnnotations;//Importerer for å validere modeller
using System.ComponentModel.DataAnnotations.Schema;//Importerer for å tilkoble modellene til databasetabeller.
using AdminDesk.Models.ServiceOrder;//Importerer for å bruke ServiceOrder-modeller i applikasjonen

namespace AdminDesk.Entities//Definerer navneområde for entitetsklasser
{
    // Definerer Verksted-klasse som tilsvarer verksted-tabell i database.

    [Table("verksted")]
    public class Verksted
    {
        public int VerkstedId { get; set; }
        public int Status { get; set; }


    }
}
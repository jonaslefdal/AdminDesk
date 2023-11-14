using System.ComponentModel.DataAnnotations;

namespace Nøsted_Serviceordre_Behandling.Models
{
    public class Serviceordre
    {

        public int? Ordrenummer { get; set; }

        [Key]
        public int id { get; set; }

        public string? Navn { get; set; }

        public string? Email { get; set; }

        public string? Telefon { get; set; }

        public string? Mottatt_dato { get; set; }

        public string? Adresse { get; set; }

        public string? ServiceordreStatus { get; set; }

        public int? Arbeidstimer { get; set; }

        public string? Reparatør { get; set; }

        public Serviceordre()
        {
            
        }
    }
}

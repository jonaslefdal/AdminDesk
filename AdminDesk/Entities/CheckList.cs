using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.CheckList;

namespace AdminDesk.Entities
{
    [Table("checklist")]
    public class CheckList
    {
        public int CheckListId { get; set; }
        public int ServiceOrderId { get; set; }

        [Required]
        public string Avd1Sp1 { get; set; }
        [Required]
        public string Avd1Sp2 { get; set; }
        [Required]
        public string Avd1Sp3 { get; set; }
        [Required]
        public string Avd1Sp4 { get; set; }
        [Required]
        public string Avd1Sp5 { get; set; }
        [Required]
        public string Avd1Sp6 { get; set; }
        [Required]
        public string Avd1Sp7 { get; set; }
        [Required]
        public string Avd1Sp8 { get; set; }
        [Required]
        public string Avd2Sp1 { get; set; }
        [Required]
        public string Avd2Sp2 { get; set; }
        [Required]
        public string Avd2Sp3 { get; set; }
        [Required]
        public string Avd2Sp4 { get; set; }
        [Required]
        public string Avd2Sp5 { get; set; }
        [Required]
        public string Avd2Sp6 { get; set; }
        [Required]
        public string Avd2Sp7 { get; set; }
        [Required]
        public string Avd3Sp1 { get; set; }
        [Required]
        public string Avd3Sp2 { get; set; }
        [Required]
        public string Avd3Sp3 { get; set; }
        [Required]
        public string Avd4Sp1 { get; set; }
        [Required]
        public string Avd5Sp1 { get; set; }
        [Required]
        public string Avd5Sp2 { get; set; }
        [Required]
        public string Avd5Sp3 { get; set; }

    }
    
}

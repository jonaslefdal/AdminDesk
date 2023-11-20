using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{
    [Table("verksted")]
    public class Verksted
    {
        public int VerkstedId { get; set; }
        public int Status { get; set; }
       

    }
}
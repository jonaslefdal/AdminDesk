using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminDesk.Models.ServiceOrder;

namespace AdminDesk.Entities
{
    [Table("UserDisabled")]
    public class User
    {
        
        public string UserId { get; set; }
        public bool Disabled { get; set; }

    }
}
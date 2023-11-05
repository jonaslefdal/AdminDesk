using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDesk.Entities
{
    [Table("Iben")]
    public class Iben
    {
        public int Id { get; set; }
        public string ?Name { get; set; }
    }
}

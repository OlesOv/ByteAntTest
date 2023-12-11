using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ByteAntTestTask.Models
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid RoleID { get; set; }
        public string Name { get; set; }
        public List<Position> Positions { get; set; } = new();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ByteAntTestTask.Models
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid EmployeeID { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public List<Position> Positions { get; } = new();
    }
}

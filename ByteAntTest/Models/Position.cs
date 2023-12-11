using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ByteAntTestTask.Models
{
    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid PositionID { get; set; }
        public Guid? EmployeeID { get; set; }
        public Guid RoleID { get; set; }
        public Guid? ReportsToID { get; set; }
        public Position? ReportsTo { get; set; }
        public Employee? Employee { get; set; }
        public Role Role { get; set; }
        public List<Position> Subordinates { get; set; }
    }
}

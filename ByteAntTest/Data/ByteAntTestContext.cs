using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ByteAntTestTask.Models;

namespace ByteAntTest.Data
{
    public class ByteAntTestContext : DbContext
    {
        public ByteAntTestContext (DbContextOptions<ByteAntTestContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<Position> Position { get; set; } = default!;
    }
}

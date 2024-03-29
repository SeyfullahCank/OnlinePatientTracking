using Department.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Department.API.Data
{
    public class DepartmentAPIContext : DbContext
    {
        public DepartmentAPIContext (DbContextOptions<DepartmentAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Department> Department { get; set; } = default!;
        public DbSet<Doctor> Doctor { get; set; } = default!;
    }
}

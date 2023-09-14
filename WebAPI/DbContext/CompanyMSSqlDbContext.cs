using Microsoft.EntityFrameworkCore;
using WebAPI.Model.Domain;

namespace WebAPI.DbContext
{
    public class CompanyMSSqlDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public CompanyMSSqlDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Department> Departments { get; set; } 

    }
}


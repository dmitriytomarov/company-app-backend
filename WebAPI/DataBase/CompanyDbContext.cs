using Microsoft.EntityFrameworkCore;
using WebAPI.Model;

namespace WebAPI.DataBase
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees{ get; set; }
        public DbSet<Department> Departments { get; set; } 

    }
}

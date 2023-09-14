using Microsoft.EntityFrameworkCore;
using WebAPI.DbContext;
using WebAPI.DbRepository.Interfaces;
using WebAPI.Model.Domain;

namespace WebAPI.DbRepository.Implementations
{
    public class DepartmentsRepositoryMSSQL : IDepartmentsRepository
    {
        private readonly CompanyMSSqlDbContext dbContext;

        public DepartmentsRepositoryMSSQL(CompanyMSSqlDbContext dbContext)
        { this.dbContext = dbContext; }

        public async Task<Department> AddDepartmentAsync(Department department)
        {
            await dbContext.Departments.AddAsync(department);
            await dbContext.SaveChangesAsync();

            return department;
        }

        public async Task<Department> GetDepartmentByNameAsync(string departmentName)
        {
            return await dbContext.Departments.FirstOrDefaultAsync(e => e.Name == departmentName);
        }

        public async Task<List<string>> GetDepartmentsNamesAsync()
        {
            return await dbContext.Departments.Select(d => d.Name).ToListAsync();
        }
    }
}

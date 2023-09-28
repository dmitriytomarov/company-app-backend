using Microsoft.EntityFrameworkCore;
using WebAPI.DbContext;
using WebAPI.DbRepository.Interfaces;
using WebAPI.Model.Domain;

namespace WebAPI.DbRepository.Implementations
{
    public class EmployeesRepositoryMSSQL : IEmployeesRepository
    {
        private readonly CompanyMSSqlDbContext dbContext;

        public EmployeesRepositoryMSSQL(CompanyMSSqlDbContext dbContext) 
        { this.dbContext = dbContext; }


        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await dbContext.Employees.Include(d => d.Department).ToListAsync();
        }


        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> DeleteEmployeeAsync(Guid employeeId)
        {
            var employee = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                await dbContext.SaveChangesAsync();
            }

            return employee;
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid employeeId)
        {
            return await dbContext.Employees.Include(d => d.Department).FirstOrDefaultAsync(e => e.Id == employeeId);
        }


        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            var updatedEmployee = dbContext.Employees.FirstOrDefault(e => e.Id == employee.Id);
            
            if (updatedEmployee != null)
            {
                dbContext.Entry(updatedEmployee).CurrentValues.SetValues(employee);
                await dbContext.SaveChangesAsync();
            }

            return updatedEmployee;
        }
    }
}

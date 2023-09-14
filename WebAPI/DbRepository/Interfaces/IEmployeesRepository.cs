using WebAPI.Model.Domain;

namespace WebAPI.DbRepository.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Employee> DeleteEmployeeAsync(int employeeId);
    }
}

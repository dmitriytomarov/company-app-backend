using WebAPI.Model.Domain;

namespace WebAPI.DbRepository.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId);
        Task<Employee> AddEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeeAsync(Employee employee);
        Task<Employee> DeleteEmployeeAsync(Guid employeeId);
    }
}

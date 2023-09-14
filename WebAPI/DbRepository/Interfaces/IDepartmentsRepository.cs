using WebAPI.Model.Domain;

namespace WebAPI.DbRepository.Interfaces
{
    public interface IDepartmentsRepository
    {
        Task<Department> GetDepartmentByNameAsync(string departmentName);

        Task<List<string>> GetDepartmentsNamesAsync();

        Task<Department> AddDepartmentAsync(Department department);
    }
}

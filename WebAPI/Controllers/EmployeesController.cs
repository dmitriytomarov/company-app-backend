using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataBase;
using WebAPI.Model;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly CompanyDbContext _db;
        public EmployeesController(CompanyDbContext db) { _db = db; }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _db.Employees.Include(d => d.Department).ToListAsync();
            var employeesDTO = new List<EmployeeDTO>();
            if (employees != null)
            {
                foreach (var employee in employees)
                {
                    employeesDTO.Add(new EmployeeDTO
                    {
                        Id = employee.Id,
                        DepartmentName = employee!.Department == null ? "" : employee!.Department!.Name,
                        Name = employee!.Name,
                        Birthday = employee.Birthday,
                        WorksFrom = employee.WorksFrom == null ? null : employee.WorksFrom,
                        Salary = employee.Salary
                    }); ;
                }
            }
            return Ok(employeesDTO);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var employee = await _db.Employees.Include(d => d.Department).FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}).");
            }

            var employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                DepartmentName = employee!.Department == null ? "" : employee!.Department!.Name,
                Name = employee!.Name,
                Birthday = employee.Birthday,
                WorksFrom = employee.WorksFrom == null ? null : employee.WorksFrom,
                Salary = employee.Salary
            };
           
            return Ok(employeeDTO);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employee)
        {
            if (employee == null) return Ok();

            var dep = await _db.Departments.FirstOrDefaultAsync(e => e.Name == employee.DepartmentName);
            int depId;

            if (dep == null)
            {
                dep = new Department { Name = employee.DepartmentName };
                await _db.AddAsync(dep);
                await _db.SaveChangesAsync();
                depId = dep.Id;
            }
            else
            {
                depId = dep.Id;
            }

            var newEmployee = new Employee
            {
                Name = employee.Name,
                Birthday = employee.Birthday,
                WorksFrom = employee.WorksFrom == null ? null : employee.WorksFrom,
                Salary = employee.Salary,
                DepartmentId = depId
            };

            await _db.AddAsync(newEmployee);
            await _db.SaveChangesAsync();
            employee.Id = newEmployee.Id;

            return Ok(employee);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDTO newEmployee)
        {
            var employee = await _db.Employees.Include(d=>d.Department).FirstOrDefaultAsync(e=>e.Id == id);
            if (employee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}). No changes made.");
            }

            employee.Name = newEmployee.Name;
            employee.Birthday = newEmployee.Birthday;
            employee.WorksFrom = newEmployee.WorksFrom == null ? null : newEmployee.WorksFrom;
            employee.Salary = newEmployee.Salary;

            if (employee.Department?.Name != newEmployee.DepartmentName)
            {
                int newDepartmentId;
                var newDep = await _db.Departments.FirstOrDefaultAsync(e => e.Name == newEmployee.DepartmentName);
                if (newDep == null)
                {
                    var dep = new Department { Name = newEmployee.DepartmentName };
                    await _db.AddAsync(dep);
                    await _db.SaveChangesAsync();
                    newDepartmentId = dep.Id;
                }
                else
                {
                    newDepartmentId = newDep.Id;
                }
                employee.DepartmentId = newDepartmentId;
            }

            await _db.SaveChangesAsync();
            newEmployee.Id = employee.Id;

            return Ok(newEmployee);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (employee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}). No changes made.");
            }

            _db.Employees.Remove(employee);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}

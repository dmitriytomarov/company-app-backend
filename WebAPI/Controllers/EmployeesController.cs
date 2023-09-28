using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebAPI.DbContext;
using WebAPI.DbRepository.Interfaces;
using WebAPI.Model;
using WebAPI.Model.Domain;
using WebAPI.Model.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesRepository employeesRepository;
        private readonly IDepartmentsRepository departmentsRepository;

        public EmployeesController(IEmployeesRepository employeesRepository, IDepartmentsRepository departmentsRepository) 
        { 
            this.employeesRepository = employeesRepository;
            this.departmentsRepository = departmentsRepository;
        }

        /// <summary>
        /// Получение всех сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await employeesRepository.GetEmployeesAsync();
            var employeesDTO = new List<EmployeeDTO>();

            //Маппинг из Employee list в EmployeeDTO list
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
                    });
                }
            }
            return Ok(employeesDTO);
        }

        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await employeesRepository.GetEmployeeByIdAsync(id);
            
            if (employee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}).");
            }

            //Маппинг из Employee в EmployeeDTO
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

        /// <summary>
        /// Создание нового сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddEmployee(AddEmployeeDTO employee)
        {
            if (employee == null) return Ok();

            //Проверим существует ли заданный департамент
            var department = await departmentsRepository.GetDepartmentByNameAsync(employee.DepartmentName);

            if (department == null)
            {
                //Создание нового департамента в базе
                department = new Department
                {
                    Name = employee.DepartmentName
                };
                department = await departmentsRepository.AddDepartmentAsync(department);
            }

            //Маппинг EmployeeDTO в Employee перед созданием нового сотрудника
            var newEmployee = new Employee
            {
                Name = employee.Name,
                Birthday = employee.Birthday,
                WorksFrom = employee.WorksFrom == null ? null : employee.WorksFrom,
                Salary = employee.Salary,
                DepartmentId = department.Id
            };

            newEmployee = await employeesRepository.AddEmployeeAsync(newEmployee);

            //Маппинг в EmployeeDTO чтобы вернуть результат
            var addedEmployee = new EmployeeDTO
            {
                Id = newEmployee.Id,
                Name = newEmployee.Name,
                Birthday = newEmployee.Birthday,
                WorksFrom = newEmployee.WorksFrom,
                Salary = newEmployee.Salary,
                DepartmentName = department.Name
            };

            return Ok(addedEmployee);
        }

        /// <summary>
        /// Обновление данных сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newEmployee"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEmployee(Guid id, UpdateEmployeeDTO newEmployee)
        {
            var department = await departmentsRepository.GetDepartmentByNameAsync(newEmployee.DepartmentName);

            if (department == null)
            {
                //Создание нового департамента в базе
                department = new Department
                {
                    Name = newEmployee.DepartmentName
                };
                department = await departmentsRepository.AddDepartmentAsync(department);
            }

            //Маппинг в Employee перед обновлением в базе
            var employeeForUpdate = new Employee
            {
                Id = id,
                Name = newEmployee.Name,
                Birthday = newEmployee.Birthday,
                WorksFrom = newEmployee.WorksFrom == null ? null : newEmployee.WorksFrom,
                Salary = newEmployee.Salary,
                DepartmentId = department.Id
            };

            var updatedEmployee = await employeesRepository.UpdateEmployeeAsync(employeeForUpdate);

            if (updatedEmployee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}). No changes made.");
            }

            //Маппинг Employee обратно в EmployeeDTO 
            var response = new EmployeeDTO
            {
                Id = updatedEmployee.Id,
                Name = updatedEmployee.Name,
                Birthday = updatedEmployee.Birthday,
                WorksFrom = updatedEmployee.WorksFrom,
                Salary = updatedEmployee.Salary,
                DepartmentName = updatedEmployee.Department!.Name
            };

            return Ok(response);
        }

        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var deletedEmployee = await employeesRepository.DeleteEmployeeAsync(id);
            
            if (deletedEmployee == null)
            {
                return NotFound($"Epmloyee is not found (id = {id}). No changes made.");
            }

            return Ok();
        }
    }
}

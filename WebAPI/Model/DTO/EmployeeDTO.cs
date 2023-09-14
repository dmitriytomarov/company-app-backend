using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; internal set; }
        public string DepartmentName { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime? WorksFrom { get; set; }
        public int Salary { get; set; }
    }
}

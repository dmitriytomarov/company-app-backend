using System.ComponentModel.DataAnnotations;

namespace WebAPI.Model.DTO
{
    public class AddEmployeeDTO
    {
        public string DepartmentName { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime? WorksFrom { get; set; }
        public int Salary { get; set; }
    }
}

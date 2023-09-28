namespace WebAPI.Model.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime? WorksFrom { get; set; }
        public int Salary { get; set; }
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}

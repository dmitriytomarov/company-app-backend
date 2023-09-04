namespace WebAPI.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime Birthday { get; set; }
        public DateTime? WorksFrom { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; } 
    }
}

using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Department
    {
        [Key]
        public Int64 DepartmentId { get; set; }
        [Required]
        [MaxLength(200)]
        public string DepartementName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}

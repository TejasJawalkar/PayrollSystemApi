using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class EmployeeSecurity
    {
        [Key]
        public Int64 UserSecurityId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public Byte[] UserPassword { get; set; }    
        public Employee employee { get; set; }
    }
}

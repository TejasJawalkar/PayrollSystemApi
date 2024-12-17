using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class EmployeeManagers
    {
        [Key]
        public Int64 EMId { get; set; }
        public Int64 EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Int64 ManagerId { get; set; }
        public ReportingManagers ReportingManagers { get; set; }
    }
}

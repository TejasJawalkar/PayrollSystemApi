
using PayrollSystem.Entity.Models.Employee;
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Employee
    {
        #region Properties
        [Key]
        public Int64 EmployeeId { get; set; }
        public Int64 OrganizationId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 PaymentID { get; set; }
        #endregion

        #region Relation References
        public EmployeeDetails EmployeeDetails { get; set; }
        public Orgnisations Orgnisations { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public EmployeeSecurity EmployeeSecurity { get; set; }
        public PaymentData PaymentData { get; set; }
        public ReportingManagers ReportingManagers { get; set; }
        public ICollection<DailyTimeSheet> DailyTimeSheets { get; set; }
        public ICollection<UserLeave> UserLeave { get; set; }
        public ICollection<EmployeeManagers> EmployeeManagers { get; set; }

        #endregion
    }
}

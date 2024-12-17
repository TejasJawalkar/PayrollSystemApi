
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Employee
    {
        #region Properties
        [Key]
        public Int64 EmployeeId { get; set; }
        [Required]
        public Int64 OrgnisationID { get; set; }
        [Required]
        public Int64 DepartmentId { get; set; }
        public Int64 ManagerID { get; set; }
        public string EmployeeName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string OrganisationEmail { get; set; }
        public string PersonalEmail { get; set; }
        [Required]
        [MaxLength(50)]
        [Phone]
        public string Mobile { get; set; }
        public string MobileNoCode { get; set; } = "";
        [Required]
        public Int64 RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsPasswordChangeActive { get; set; } = true;
#endregion

        #region Relation References
        public Orgnisations Orgnisations { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public PaymentData PaymentData { get; set; }
        public EmployeeSecurity EmployeeSecurity { get; set; }
        public ReportingManagers ReportingManagers { get; set; }
        public ICollection<UserLeave> UserLeaves { get; set; }
        public ICollection<DailyTimeSheet> DailyTimeSheets { get; set; } 
        public EmployeeManagers EmployeeManagers { get; set; } 
        #endregion
    }
}

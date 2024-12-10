using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Employee
    {
        [Key]
        public Int64 EmployeeId { get; set; }

        [Required]
        public Int64 OrgnisationID { get; set; }

        [Required]
        public Int64 DepartmentId { get; set; }

        public string EmployeeName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string OrganisationEmail { get; set; }

        public string PersonalEmail { get; set; }

        [Phone]
        public string Mobile { get; set; }

        public string MobileNoCode { get; set; } = null;

        public int Role { get; set; }

        public bool IsActive { get; set; } = true;

        public Orgnisations orgnisations { get; set; }
        public Department Departments { get; set; }
        public ICollection<PaymentData> paymentDatas { get; set; }
        public ICollection<EmployeeSecurity> employeeSecurities { get; set; }
        public ICollection<DailyTimeSheet> dailyTimeSheets { get; set; }
        public ICollection<UserLeave> userLeaves { get; set; } 
    }
}

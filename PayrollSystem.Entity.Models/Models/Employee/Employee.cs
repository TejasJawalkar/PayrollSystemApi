using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Employee
    {
        [Key]
        public Int64 EmployeeId { get; set; }

        [Required]
        public Int64 OrgnisationID { get; set; }
        
        public string EmployeeName { get; set; }

        [Required]
        [EmailAddress]
        public string OrganisationEmail { get; set; }

        public string PersonalEmail { get; set; }

        [Phone]
        public string Mobile { get; set; }

        public string MobileNoCode { get; set; } = null;

        public int Role { get; set; }

        public bool IsActive { get; set; } = true;

        public Orgnisations orgnisations { get; set; }
        public ICollection<PaymentData> paymentDatas { get; set; }
        public ICollection<EmployeeSecurity> employeeSecurities { get; set; }
        public ICollection<DailyTimeSheet> dailyTimeSheets { get; set; }
    }
}

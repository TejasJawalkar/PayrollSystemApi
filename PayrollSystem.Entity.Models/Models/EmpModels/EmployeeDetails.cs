using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Entity.Models.Employee
{
    public class EmployeeDetails
    {
        #region Properties
        [Key]
        public Int64 EmployeeDetails_Id{get;set;}
        public Int64 EmployeeId { get; set; }
        [Required]
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
        public bool IsActive { get; set; } = true;
        public bool IsPasswordChangeActive { get; set; } = true;
        #endregion

        #region Relation References
        public Employee Employee { get; set; }
        #endregion
    }
}

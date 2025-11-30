using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class EmployeeSecurity
    {
        #region Properties
        [Key]
        public Int64 UserSecurityId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public Byte[] UserPassword { get; set; }
        #endregion

        #region Relation References
        public Employee Employee { get; set; }
        #endregion
    }
}

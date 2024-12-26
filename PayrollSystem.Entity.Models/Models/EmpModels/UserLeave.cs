using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class UserLeave
    {
        [Key]
        public Int64 LeaveId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public DateTime AppliedDate { get; set; }
        [Required]
        [MaxLength(10)]
        public Int32 Status { get; set; }
        public Int32 ApprovedBy { get; set; }
        [Required]
        [MaxLength(500)]
        public string AppliedReason { get; set; }
        [Required]
        [MaxLength(500)]
        public string RejectedReason { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public Double NoofDays { get; set; }

        #region 
        public Employee Employee    { get; set; }
        #endregion
    }
}

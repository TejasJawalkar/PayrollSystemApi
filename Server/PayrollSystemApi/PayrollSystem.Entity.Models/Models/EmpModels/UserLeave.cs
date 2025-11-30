#region Imports
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace PayrollSystem.Entity.Models.Employee
{
    public class UserLeave
    {
        #region Properties
        [Key]
        [NotNull]
        public Int64 LeaveId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public DateTime AppliedDate { get; set; }
        [Required]
        [MaxLength(10)]
        public Int32 Status { get; set; } = 1;
        [AllowNull]
        public Int32 ApprovedBy { get; set; }
        [Required]
        [MaxLength(500)]
        public string? AppliedReason { get; set; }
        [AllowNull]
        [MaxLength(500)]
        public string? RejectedReason { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public Double NoofDays { get; set; }
        [AllowNull]
        public Boolean IsArchived { get; set; } = false;
        [AllowNull]
        public Boolean IsApproved { get; set; } = false;
        [AllowNull]
        public Boolean IsRejected { get; set; } = false;
        #endregion

        #region ForeignKeyReferences
        public Employee Employee { get; set; }
        #endregion
    }
}

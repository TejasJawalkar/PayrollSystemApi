using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PayrollSystem.Entity.Models.Employee
{
    public class DailyTimeSheet
    {
        #region Properties
        [Key]
        public Int64 TimeSheetId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public DateTime LoginDate { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        [Required]
        public String LoginLocation { get; set; }
        [AllowNull]
        public DateTime? LogOutDate { get; set; }
        [AllowNull]
        public DateTime? LogOutTime { get; set; }
        [AllowNull]
        public String? LogoutLocation { get; set; }
        [AllowNull]
        public Double? TotalHoursWorked { get; set; }
        [AllowNull]
        public String? AttendanceFlag { get; set; }
        #endregion

        #region ForeignLKey References
        public Employee Employee { get; set; }
        #endregion

    }
}

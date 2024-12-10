using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class DailyTimeSheet
    {
        [Key]
        public Int64 TimeSheetId { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }
        [Required]
        public DateTime TodayDate { get; set; }
        [Required]
        public DateTime LoginTime { get; set; }
        [Required]
        public String LoginLocation { get; set; }
        public DateTime LogOutTime { get; set; }
        public String LogoutLocation { get; set; }
        public Int32 TotalHoursWorked { get; set; }
        public String AttendanceFlag {get;set;}
        public Employee employee { get; set; }
    }
}

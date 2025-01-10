using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.Entity.InputOutput.Employee
{
    #region EmployeeDetails
    public class EmployeeDetails
    {
        public String EmployeeName { get; set; }
        public String OrganisationEmail { get; set; }
        public String PersonalEmail { get; set; }
        public String Mobile { get; set; }
        public String Role { get; set; }
        public Int32 Stamp { get; set; }
        public String DepartementName { get; set; }
        public String OrgnizationName { get; set; }
        public Boolean IsActive { get; set; } = true;
    }
    #endregion

    #region EmployeeLoginInput
    public class EmployeeLoginInput
    {
        [FromForm] public String UserName { get; set; }
        [FromForm] public String Password { get; set; }
    }
    #endregion

    #region EmployeeFormInput
    public class EmployeeFormInput
    {
        [FromForm] public Int64? EmployeeId { get; set; }
        [FromForm] public Int64? OrganisationId { get; set; }
        [FromForm] public DateTime? CurrentDate { get; set; }
    }
    #endregion

    #region LoginLogoutFormInput
    public class LoginLogoutFormInput
    {
        [FromForm] public Int64 EmployeeId { get; set; }
        [FromForm] public DateTime LoginDate { get; set; }
        [FromForm] public DateTime LoginTime { get; set; }
        [FromForm] public String LoginLocation { get; set; }
        [FromForm] public DateTime LogOutDate { get; set; }
        [FromForm] public DateTime LogOutTime { get; set; }
        [FromForm] public String LogoutLocation { get; set; } = "";
        [FromForm] public Double TotalHoursWorked { get; set; }
    }
    #endregion

    #region UserLeavesInput
    public class UserLeaveInput
    {
        [FromForm] public Int64 EmployeeId { get; set; }
        [FromForm] public DateTime AppliedDate { get; set; }
        [FromForm] public Int32 Status { get; set; } = 1;
        [FromForm] public String? AppliedReason { get; set; }
        [FromForm] public DateTime FromDate { get; set; }
        [FromForm] public DateTime ToDate { get; set; }
        [FromForm] public Double? NoofDays { get; set; }
    }
    #endregion

    #region 
    #endregion 

}

using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.Entity.InputOutput.Employee
{
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

    public class EmployeeLoginInput
    {
        [FromForm] public String UserName { get; set; }
        [FromForm] public String Password { get; set; }
    }

    public class EmployeeFormInput
    {
        [FromForm] public Int64? EmployeeId { get; set; }
        [FromForm] public Int64? OrganisationId { get; set; }
        [FromForm] public DateTime? CurrentDate { get; set; }
    }

    public class LoginLogoutFormInput
    {
        public Int64 EmployeeId { get; set; }
        public DateTime LoginDate { get; set; }
        public DateTime LoginTime { get; set; }
        public String LoginLocation { get; set; }
        public DateTime LogOutDate { get; set; }
        public DateTime LogOutTime { get; set; }
        public String LogoutLocation { get; set; } = "";
        public Double TotalHoursWorked { get; set; }
    }
}

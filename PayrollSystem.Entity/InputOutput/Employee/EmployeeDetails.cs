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
}

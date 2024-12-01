using Microsoft.AspNetCore.Mvc;

namespace PayrollSystem.Entity.InputOutput.HR
{
    public class NewEmployeeInput
    {
        [FromForm] public Int64 OrgnisationID { get; set; }
        [FromForm] public String EmployeeName { get; set; }
        [FromForm] public String OrganisationEmail { get; set; }
        [FromForm] public String PersonalEmail { get; set; }
        [FromForm] public String Mobile { get; set; }
        [FromForm] public Int32 Role { get; set; }
        [FromForm] public Boolean IsActive { get; set; }=true;
    }
}

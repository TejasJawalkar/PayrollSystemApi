using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace PayrollSystem.Entity.InputOutput.HR
{
    public class NewEmployeeInput
    {
        [FromForm] public Int64 OrgnisationID { get; set; }
        [FromForm] public Int64 DepartmentId { get; set; }
        [FromForm] public Int64 EmployeeId { get; set; }
        [FromForm] public Int64 RoleId { get; set; }
        [FromForm] public String EmployeeName { get; set; }
        [FromForm] public String OrganisationEmail { get; set; }
        [FromForm] public String PersonalEmail { get; set; }
        [FromForm] public String Mobile { get; set; }
        [FromForm] public Double MobileNoCode { get; set; }
        [FromForm] public Double CTC { get; set; }
        [FromForm] public Double GrossPay { get; set; }
        [FromForm] public Double NetPay { get; set; }
    }
}

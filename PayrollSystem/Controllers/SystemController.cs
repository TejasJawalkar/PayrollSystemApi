using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.Models.Employee;

namespace PayrollSystem.Controllers
{
    public class SystemController : Controller
    {
        private readonly DbsContext _bsContext;
        public SystemController(DbsContext dbsContext)
        {
            _bsContext = dbsContext;
        }
        [HttpPost]
        [Route("System/AddOrganization")]
        public IActionResult AddOrganization()
        {
            try
            {
                var organization = new Orgnisations
                {
                    OrgnizationName = "Global Innovations",
                    OrgnisationAddress = "456 Elm Street",
                    OrgnisationCountry = "USA",
                    OrgnisationState = "California",
                    OrgnisationPincode = "90210",
                    OrgnisationStartDate = Convert.ToDateTime("2023-12-01"),
                    OrgnisationDirectorName = "John Doe",
                    DirectorMobileNo = "9876543210",
                    DirectorEmail = "johndoe@example.com",
                    OrgnisationCeo = "Alice Smith",
                    CeoMobileNo = "9871234560",
                    CeoEmail = "AliceSmith@example.com",
                    OrgnisationGstNo = "12BBCC1234C1A5",
                    OrgnisationStartTime = "09:00:00",
                    OrgnisationEndTime = "17:00:00",
                    IsActive = true,
                    SystemRegisteredDate = DateTime.Now,
                };
                _bsContext.Oragnizations.Add(organization);
                _bsContext.SaveChanges();
                return Ok(new {message="Organisation Saved"});
            }
            catch (Exception)
            {
                return StatusCode(500,"Internal Server Error");
            }
        }

        [HttpPost]
        [Route("System/AddRoles")]
        public IActionResult AddRoles([FromForm ]String RoleName )
        {
            try
            {
                var roles = new Designation
                {
                    Role = RoleName,
                };

                _bsContext.Roles.Add(roles);
                _bsContext.SaveChanges();
                return Ok(new {message="Role Added"});
            }
            catch (Exception)
            {
                return StatusCode(500,new {message="Internal Server Error"});
            }
        }

        [HttpPost]
        [Route("System/AddDepartments")]
        public IActionResult AddDepartments([FromForm] String departmentNm)
        {
            try
            {
                var department = new Department
                {
                    DepartementName = departmentNm
                };
                _bsContext.Departments.Add(department);
                _bsContext.SaveChanges();
                return Ok(new { message = "Department Added" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }
    }
}

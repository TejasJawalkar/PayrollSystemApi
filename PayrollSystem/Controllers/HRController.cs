using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PayrollSystem.Business.Common;
using PayrollSystem.Business.HR;
using PayrollSystem.Core.HR;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.HR;


namespace PayrollSystem.Controllers
{

    public class HRController : Controller
    {
        private readonly IBussHrServices _hrServices;
        private readonly IBussCommonServices _bussCommonServices;

        public HRController(IBussHrServices hrServices, IBussCommonServices bussCommonServices)
        {
            _hrServices = hrServices;
            _bussCommonServices = bussCommonServices;
        }

        [HttpPost]
        [Route("/Admin/RegisterNewEmployee")]
        [Authorize(Roles ="2,3,4,5")]
        public async Task<JsonResult> RegisterNewEmployee(NewEmployeeInput newEmployeeInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 EmployeeId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                if (EmployeeId != 0)
                {
                    if (newEmployeeInput.OrgnisationID != 0 || newEmployeeInput.DepartmentId != 0 || !string.IsNullOrEmpty(newEmployeeInput.OrganisationEmail) || !string.IsNullOrEmpty(newEmployeeInput.EmployeeName) | !string.IsNullOrEmpty(newEmployeeInput.PersonalEmail) || !string.IsNullOrEmpty(newEmployeeInput.Mobile) || newEmployeeInput.RoleId != 0)
                    {
                        await _hrServices.RegisterNewEmployee(newEmployeeInput, response);
                    }
                    else
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.EmptyData;
                        response.Message = "Something is missing";
                    }
                }
                else
                {
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnAuthorized;
                    response.Message = "Token is Empty";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }

        [HttpPost]
        [Route("/Admin/GetAllEmployee")]
        [Authorize(Roles ="1,2,3,4")]
        public async Task<JsonResult> GetAllEmployee()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 OrganisationId = Convert.ToInt64(currentUser.Claims.First(c=>c.Type== "OrganisationId").Value);
                Int64 EmployeeId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                if(EmployeeId!=0)
                {
                    if (OrganisationId != 0) 
                    {
                        await _bussCommonServices.GetAllEmployee(OrganisationId,response);
                    }
                }
                else
                {
                    response.Message += "Invalid Token Access";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnAuthorized;
                }
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }

    }
}

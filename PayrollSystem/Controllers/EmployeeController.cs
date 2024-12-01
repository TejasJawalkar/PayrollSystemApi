using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Business.Common;
using PayrollSystem.Business.Employee;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;


namespace PayrollSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IBussEmployeeServices _bussEmployeeServices;
        private readonly IBussCommonServices _bussCommonServices;
        public EmployeeController(IBussEmployeeServices bussEmployeeServices,IBussCommonServices bussCommonServices)
        {
            _bussEmployeeServices=bussEmployeeServices;
            _bussCommonServices=bussCommonServices;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Employee/Login")]
        public async Task<JsonResult> EmployeeLogin(EmployeeLoginInput employeeLoginInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                if (String.IsNullOrEmpty(employeeLoginInput.UserName) && String.IsNullOrEmpty(employeeLoginInput.Password))
                {
                    response.Message += "UserName and Password is Required";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.EmptyData;
                }
                else 
                { 
                    await _bussEmployeeServices.EmployeeLogin(employeeLoginInput, response);
                }
               
            }
            catch (Exception ex)
            {
                response.ObjectStatusCode=Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message = "Internal Server Error";
            }
            return Json(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/Employee/NewPassword")]
        public async Task<JsonResult> NewRegister([FromForm] String EmailId, [FromForm] String Password)
        {
            ResponseModel response=new ResponseModel();
            try
            {
                await _bussEmployeeServices.NewRegister( EmailId, Password, response);
            }
            catch (Exception ex)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }

        [HttpPost]
        [Authorize(Roles ="1,2,3,4,5")]
        [Route("/Employee/EmployeeDetails")]
        public async Task<JsonResult> GetEmployee([FromForm] Int64 EmployeeId, [FromForm] Int64 OrgnisationId)
        {ResponseModel response= new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 EId = Convert.ToInt64(currentUser.Claims.First(c=>c.Type== "EmployeeId").Value);
                Int64 OId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "OrganisationId").Value);
                if (EId!=0) 
                {
                    _bussCommonServices.GetEmployee(EmployeeId, OrgnisationId, response);
                }
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
         
        [Route("/Employee/GetAllEmployee")]
        [HttpPost]
        [Authorize(Roles ="1,2,3,4,5")]
        public async Task<JsonResult> GetAllEmployee()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 OrganisationId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "OrganisationId").Value);
                Int64 EmployeeId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                if (EmployeeId != 0)
                {
                    if (OrganisationId != 0)
                    {
                        await _bussCommonServices.GetAllEmployee(OrganisationId, response);
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

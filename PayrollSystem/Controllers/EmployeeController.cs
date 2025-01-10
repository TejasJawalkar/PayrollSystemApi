#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Business.Employee;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
#endregion

namespace PayrollSystem.Controllers
{
    public class EmployeeController : Controller
    {
        #region ObjectDeclaration
        private readonly IBussEmployeeServices _bussEmployeeServices;
        #endregion

        #region Constructor
        public EmployeeController(IBussEmployeeServices bussEmployeeServices)
        {
            _bussEmployeeServices = bussEmployeeServices;
        }
        #endregion

        #region EmployeeLogin
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
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message = "Internal Server Error";
            }
            return Json(response);
        }
        #endregion

        #region NewRegister
        [HttpPost]
        [AllowAnonymous]
        [Route("/Employee/NewPassword")]
        public async Task<JsonResult> NewRegister([FromForm] String EmailId, [FromForm] String Password)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _bussEmployeeServices.NewRegister(EmailId, Password, response);
            }
            catch (Exception ex)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion

        #region SignInStatus
        /// <summary>
        /// Get Sign In Status for change the button text on ui for sing In or sing Out  
        /// </summary>
        /// <param name="employeeFormInput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>

        [HttpPost]
        [Route("Employee/GetLoginStatus")]
        [Authorize]
        public async Task<JsonResult> SignInStatus(EmployeeFormInput employeeFormInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _bussEmployeeServices.SignInStatus(employeeFormInput, response);
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion

        #region AddUpdateSignInSignOut
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginLogoutFormInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Employee/AddSignInSignOut")]
        [Authorize]
        public async Task<JsonResult> AddUpdateSignInSignOut(LoginLogoutFormInput loginLogoutFormInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _bussEmployeeServices.AddUpdateSignInSignOut(loginLogoutFormInput, response);
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion

        #region AddNewLeave
        /// <summary>
        /// Adding New Leave of Employee
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Employee/AddLeave")]
        [Authorize]
        public async Task<JsonResult> AddNewLeave(UserLeaveInput userLeaveInput)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _bussEmployeeServices.AddNewLeave(userLeaveInput, response);
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }

        #endregion

        #region GetLeaveStatus
        /// <summary>
        /// Employee Will See the Leave Status that is Approved or Pending
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Employee/GetLeaveStatus")]
        [Authorize]
        public async Task<JsonResult> GetLeaveStatus(Int64 EmployeeId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 employeeId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                if (employeeId != 0)
                {
                    await _bussEmployeeServices.GetLeaveStatus(employeeId, response);
                }
                else
                {
                    response.Message += "UnAuthorized Access";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnAuthorized;
                }
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion
    }
}

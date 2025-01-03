﻿#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Business.Common;
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
    }
}

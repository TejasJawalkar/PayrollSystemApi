﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Business.Common;
using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Controllers
{
    public class CommonController : Controller
    {
        #region Object Declaration
        private readonly IBussCommonTaskServices _BussCommonTaskServices;
        #endregion

        #region Constructor
        public CommonController(IBussCommonTaskServices BussCommonTaskServices)
        {
             _BussCommonTaskServices = BussCommonTaskServices;
        }
        #endregion

        #region GetRoles
        [HttpPost]
        [Authorize]
        [Route("/GetRoles")]
        public async Task<JsonResult> GetRoles()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _BussCommonTaskServices.GetRoles(response);
            }
            catch (Exception)
            {
                response.Message += "internal server error, please try later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion

        #region GetDepartments
        [HttpPost]
        [Authorize]
        [Route("/GetDepartments")]
        public async Task<JsonResult> GetDepartments()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                await _BussCommonTaskServices.GetDepartments(response);
            }
            catch (Exception)
            {
                response.Message += "internal server error, please try later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion

        #region GetAllEmployee
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("/Employee/AllEmployee")]
        [Authorize]
        public async Task<JsonResult> GetAllEmployee()
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 OrganisationId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "OrganisationId").Value);
                Int64 EmployeeId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                if (OrganisationId != 0)
                {
                    if (OrganisationId != 0)
                    {
                        await _BussCommonTaskServices.GetAllEmployee(OrganisationId, response);
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

        #endregion

        #region GetEmployee
        [HttpPost]
        [Authorize]
        [Route("/Employee/EmployeeDetails")]
        public async Task<JsonResult> GetEmployee([FromForm] Int64 EmployeeId)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                var currentUser = HttpContext.User;
                Int64 EId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "EmployeeId").Value);
                Int64 OId = Convert.ToInt64(currentUser.Claims.First(c => c.Type == "OrganisationId").Value);
                if (EId != 0)
                {
                    await _BussCommonTaskServices.GetEmployee(EmployeeId, OId, response);
                }
            }
            catch (Exception)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
            return Json(response);
        }
        #endregion
    }
}

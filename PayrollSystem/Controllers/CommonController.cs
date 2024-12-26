using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Controllers
{
    public class CommonController : Controller
    {

        public CommonController()
        {
            
        }
        //[Authorize]
        //[Route("/GetRoles")]
        //public async Task<JsonResult> GetRoles()
        //{
        //    ResponseModel response = new ResponseModel();
        //    try
        //    {
                 
        //    }
        //    catch (Exception)
        //    {
        //        response.Message += "Internal Server Error, Please try Later";
        //        response.ObjectStatusCode=Entity.InputOutput.Common.StatusCodes.UnknowError;
        //    }
        //    return Json(response);
        //}
    }
}

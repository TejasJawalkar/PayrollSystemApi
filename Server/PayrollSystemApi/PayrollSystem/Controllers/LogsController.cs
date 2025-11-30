using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Business.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Logs;

namespace PayrollSystem.Controllers
{
    public class LogsController : Controller
    {
		private readonly IBussLogServices _bussLogServices;

        public LogsController(IBussLogServices bussLogServices)
        {
            _bussLogServices = bussLogServices;
        }

		[HttpPost]
		[Route("/LoginUser/UserLogs")]
		[AllowAnonymous]

        public async Task<JsonResult> InsertUserLogs(UserLogInput userLogInput)
        {
			ResponseModel response = new ResponseModel();
			try
			{
				await _bussLogServices.InsertUserLogs(userLogInput,response);
			}
			catch (Exception ex)
			{
				response.Message += "Internal Server Error";
				response.ObjectStatusCode=Entity.InputOutput.Common.StatusCodes.UnknowError;
			}
			return Json(response);
        }

        [HttpPost]
        [Route("/UiExceptionLog")]
        [AllowAnonymous]
        public async Task<JsonResult> InsertUiExceptionLog(UiExceptionLogInput uiExceptionLogInput)
		{
			ResponseModel response = new ResponseModel();
			try
			{
				await _bussLogServices.InsertUiExceptionLog(uiExceptionLogInput,response);
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

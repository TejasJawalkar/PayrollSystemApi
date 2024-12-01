using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Controllers
{
    public class LogsController : Controller
    {
        public async Task<JsonResult> InsertUserLogs()
        {
			ResponseModel response = new ResponseModel();
			try
			{

			}
			catch (Exception ex)
			{
				throw ex;
			}
			return Json(response);
        }
    }
}

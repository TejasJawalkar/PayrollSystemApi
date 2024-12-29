using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PayrollSystem.Business.Common;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.HR;
using PayrollSystem.Core.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.HR;

namespace PayrollSystem.Business.HR
{
    public class BussHRServices : IBussHrServices
    {
        #region Objective Declaration
        private readonly IHrServices _hrServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogServices _logger;
        #endregion

        #region Constructor
        public BussHRServices(IHrServices hrServices, IHttpContextAccessor httpContextAccessor, ILogServices logger)
        {
            _hrServices = hrServices;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion

        #region RegisterNewEmployee
        public async Task RegisterNewEmployee(NewEmployeeInput newEmployee, ResponseModel response)
        {
			Int32 Result;
			try
			{
                Result = await _hrServices.RegisterNewEmployee(newEmployee,response);
                if(response.ObjectStatusCode!=Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (Result ==1)
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                        response.Message += "Employee Created";
                    }
                    else if(Result==1) {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Exists;
                        response.Message += "Employee Already Exists";
                    }
                    else
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                        response.Message += "Employee Not Registered";
                    }
                }
               
                response.Data = Result;
			}
			catch (Exception ex)
			{
                await _logger.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }
        #endregion
    }
}

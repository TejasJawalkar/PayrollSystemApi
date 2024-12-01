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
    public class BussHRServices : IBussHrServices, IBussCommonServices
    {
        private readonly IHrServices _hrServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogServices _logger;
        private readonly ICommonServices _commonServices;
        public BussHRServices(IHrServices hrServices,IHttpContextAccessor httpContextAccessor,ILogServices logger,ICommonServices commonServices)
        {
            _hrServices = hrServices;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _commonServices = commonServices;
        }

        public async Task GetAllEmployee(Int64 OrganisationId,ResponseModel response)
        {
            OutputList outputList = new OutputList(); 
            try
            {
                outputList = await _commonServices.GetAllEmployee(OrganisationId,response);
                if(response.ObjectStatusCode!=Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (outputList.EmployeeDetails.Count!=0)
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                        response.Message = "Employee List";

                        response.Data = outputList.EmployeeDetails;
                    }
                    else
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                        response.Message = "No Employee's Found";
                    }
                }
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

        public async Task RegisterNewEmployee(NewEmployeeInput newEmployee, ResponseModel response)
        {
			Int32 Result;
			try
			{
                Result = await _hrServices.RegisterNewEmployee(newEmployee,response);
                if(response.ObjectStatusCode!=Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (Result == 0)
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                        response.Message += "Employee Registered";
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

        public async Task GetEmployee(Int64 Id, Int64 OrgnisationId, ResponseModel response) 
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                employeeDetails = await _commonServices.GetEmployee(Id, OrgnisationId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    response.Message = "Employee Data";
                }
                response.Data = employeeDetails;
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
    }
}

using PayrollSystem.Core.Common;
using PayrollSystem.Core.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;

namespace PayrollSystem.Business.Common
{
    public class BussCommonTaskServices  : IBussCommonTaskServices
    {
        #region Object Declaration
        private readonly ILogServices _logServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonTaskServices _CommonTaskServices;
        #endregion
        
        #region Constructor
        public BussCommonTaskServices(ICommonTaskServices CommonTaskServices, ILogServices logServices, IHttpContextAccessor httpContextAccessor)
        {
            _CommonTaskServices = CommonTaskServices;
            _logServices = logServices;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region GetDepartments
        public async Task GetDepartments(ResponseModel response)
        {
            OutputList outputList = new OutputList();
            try
            {
                outputList = await _CommonTaskServices.GetDepartments(response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    response.Message += "Departments List";
                }
                response.Data = outputList;
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion

        #region GetRoles
        public async Task GetRoles(ResponseModel response)
        {
            OutputList outputList = new OutputList();   
            try
            {
                outputList= await _CommonTaskServices.GetRoles(response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError) 
                {
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    response.Message += "Roles List";
                }
                response.Data = outputList;
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion

        #region GetAllEmployee
        public async Task GetAllEmployee(long OrganisationId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            try
            {
                outputList = await _CommonTaskServices.GetAllEmployee(OrganisationId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (outputList.EmployeeDetails.Count != 0)
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
                await _logServices.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }
        #endregion

        #region GetEmployee
        public async Task GetEmployee(long EmployeeId, long OrgnisationId, ResponseModel response)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                employeeDetails = await _CommonTaskServices.GetEmployee(EmployeeId, OrgnisationId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.Message += "Employee Details";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
               
                response.Data = employeeDetails;
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name,
                  Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                  ex.Message,
                  _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion
    }
}

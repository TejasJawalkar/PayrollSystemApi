using Dapper;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.Logs;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.HR;

namespace PayrollSystem.Core.HR
{
    public class HRServices : IHrServices
    {
        #region Object Declaration
        private readonly IConfiguration _configuration;
        private readonly DapperDbContext _dapperDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogServices _logger;
        #endregion

        #region Constructor
        public HRServices(IConfiguration configuration, DapperDbContext dapperDbContext, IHttpContextAccessor httpContextAccessor, ILogServices logger)
        {
            _configuration = configuration;
            _dapperDbContext = dapperDbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion

        #region RegisterNewEmployee
        public async Task<Int32> RegisterNewEmployee(NewEmployeeInput newEmployee, ResponseModel response)
        {
            Int32 Result = 0;
            try
            {
                var procedure = "RegisterNewEmployee";

                var parameters = new DynamicParameters();
                parameters.Add("OrgnisationID", newEmployee.OrgnisationID, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("DepartmentId", newEmployee.DepartmentId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("EmployeeId", newEmployee.EmployeeId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("RoleId", newEmployee.RoleId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("EmployeeName", newEmployee.EmployeeName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("OrganisationEmail", newEmployee.OrganisationEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("PersonalEmail", newEmployee.PersonalEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Mobile", newEmployee.Mobile, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("MobileNoCode", newEmployee.MobileNoCode, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("CTC", newEmployee.CTC, System.Data.DbType.Double, System.Data.ParameterDirection.Input);
                parameters.Add("GrossPay", newEmployee.GrossPay, System.Data.DbType.Double, System.Data.ParameterDirection.Input);
                parameters.Add("NetPay", newEmployee.NetPay, System.Data.DbType.Double, System.Data.ParameterDirection.Input);
                parameters.Add("result", System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                using (var con = _dapperDbContext.CreateConnection())
                {
                    Result = await con.QueryFirstOrDefaultAsync<Int32>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure).ContinueWith(e=> parameters.Get<Int32>("result"));
                }
            }
            catch (Exception ex)
            {
                await _logger.InsertExceptionLogs(this.GetType().Name,
                     Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                     ex.Message,
                     _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Registered\";";
            }
            return Result;
        }
        #endregion
    }
}

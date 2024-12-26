#region Imports
using Dapper;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.Logs;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
#endregion

namespace PayrollSystem.Core.Employee
{
    public class EmployeeServices : IEmployeeServices,ICommonServices
    {
        #region Objects
        private readonly DapperDbContext _dapperDbContext;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogServices _logServices;
        #endregion

        #region Constructor
        public EmployeeServices(DapperDbContext dapperDbContext,IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogServices logServices)
        {
            _configuration = configuration; 
            _dapperDbContext = dapperDbContext; 
            _httpContextAccessor = httpContextAccessor; 
            _logServices = logServices;
        }
        #endregion

        #region EmployeeLogin
        public async Task<TokenOutput> EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response)
        {
            TokenOutput tokenOutput = new TokenOutput();
            try
            {
                var procedure = "LoginEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("UserName", employeeLoginInput.UserName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Password", employeeLoginInput.Password, System.Data.DbType.String,System.Data.ParameterDirection.Input);

                using (var con=_dapperDbContext.CreateConnection())
                {
                    tokenOutput = await con.QueryFirstOrDefaultAsync<TokenOutput>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name,Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]),ex.Message,_httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return tokenOutput;
        }
        #endregion

        #region GetAllEmployee
        public async Task<OutputList> GetAllEmployee(long OrgnisationId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            outputList.EmployeeDetails = new List<EmployeeDetails>();

            try  
            {
                var procedure = "GetAllEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrganisationId", OrgnisationId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                using (var con = _dapperDbContext.CreateConnection())
                {
                    var reader = await con.QueryMultipleAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        outputList.EmployeeDetails = reader.Read<EmployeeDetails>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logServices.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Found";
            }
            return outputList;
        }
        #endregion

        #region GetEmployee
        public async Task<EmployeeDetails> GetEmployee(Int64 Id, Int64 OrgnisationId, ResponseModel response)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                var procedure = "GetEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrgnisationId", OrgnisationId,System.Data.DbType.Int64,System.Data.ParameterDirection.Input);
                parameters.Add("Id", Id,System.Data.DbType.Int64,System.Data.ParameterDirection.Input);
                using (var con=_dapperDbContext.CreateConnection())
                {
                    employeeDetails = await con.QueryFirstOrDefaultAsync<EmployeeDetails>(procedure,parameters,commandType:System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return employeeDetails;
        }
        #endregion

        #region NewRegister
        public async Task<Int32> NewRegister(string EmailId, string Password, ResponseModel response)
        {
            Int32 result = 0;
            try
            {
                var procedure = "RegeisterNewPassword";
                var parameters = new DynamicParameters();
                parameters.Add("EmailId", EmailId, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Password", Password, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("result", System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                using (var con = _dapperDbContext.CreateConnection())
                {
                    result = await con.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure).ContinueWith(t => parameters.Get<Int32>("result"));
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return result;
        }
        #endregion
    }
}

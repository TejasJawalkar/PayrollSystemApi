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
    public class EmployeeServices : IEmployeeServices
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
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return tokenOutput;
        }

        #endregion

        #region GetTodaySignInStatus
        /// <summary>
        /// Get Single Day Sign In Status for Employee for toggle text of sign In/sign out button on browser
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <param name="TodayDate"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task<Int32> GetTodaySignInStatus(long EmployeeId, DateTime TodayDate, ResponseModel response)
        {
            Int32 Result = 0;
            try
            {
                var storedprocedure= "GetSignInStatusofDay";
                var parameters = new DynamicParameters();
                parameters.Add("EmployeeId", EmployeeId,System.Data.DbType.Int64,System.Data.ParameterDirection.Input);
                parameters.Add("TodayDate", TodayDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                parameters.Add("result",direction: System.Data.ParameterDirection.Input);
                using (var con=_dapperDbContext.CreateConnection())
                {
                    Result = await con.QueryMultipleAsync(storedprocedure,parameters,commandType:System.Data.CommandType.StoredProcedure).ContinueWith(e=> parameters.Get<Int32>("result"));
                }
            }
            catch (Exception ex)
            {
                response.Message += ex.Message;
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return Result;
        }
        #endregion

        #region NewRegister
        /// <summary>
        /// RegeisterNewPassword
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="response"></param>
        /// <returns></returns>
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
                await _logServices.InsertExceptionLogs( Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return result;
        }
        #endregion
    }
}

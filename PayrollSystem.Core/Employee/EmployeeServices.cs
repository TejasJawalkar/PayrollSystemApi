#region Imports
using Dapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Core.Logs;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
using PayrollSystem.Entity.Models.Employee;
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
        private readonly DbsContext _dbsContext;
        #endregion

        #region Constructor
        public EmployeeServices(DapperDbContext dapperDbContext, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogServices logServices, DbsContext dbsContext)
        {
            _configuration = configuration;
            _dapperDbContext = dapperDbContext;
            _httpContextAccessor = httpContextAccessor;
            _logServices = logServices;
            _dbsContext = dbsContext;
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
                parameters.Add("Password", employeeLoginInput.Password, System.Data.DbType.String, System.Data.ParameterDirection.Input);

                using (var con = _dapperDbContext.CreateConnection())
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
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return result;
        }
        #endregion

        #region SignInStatus
        public async Task<Int32> SignInStatus(EmployeeFormInput employeeFormInput, ResponseModel response)
        {
            Int32 Result = 0;
            try
            {
                var data = _dbsContext.DailyTimeSheet.Where(r => r.EmployeeId == employeeFormInput.EmployeeId && r.LoginDate == employeeFormInput.CurrentDate).FirstOrDefault();
                if (data == null)
                {
                    Result = 3;
                }
                else
                {
                    Result = 1;
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return await Task.FromResult(Result);
        }
        #endregion

        #region TotalHoursWorkedCalculation
        public async Task<DailyTimeSheet> TotalHoursWorkedCalculation(Int64 EmployeeId, DateTime LoginDate)
        {
            DailyTimeSheet data = null;
            try
            {
                data = await _dbsContext.DailyTimeSheet.Where(r => r.EmployeeId == EmployeeId && r.LoginDate == LoginDate).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message.ToString(), _httpContextAccessor.HttpContext.Request.Host.Value.Trim().ToString());
                return null;
            }
        }
        #endregion

        #region AddUpdateSignInSignOut
        public async Task<Int32> AddUpdateSignInSignOut(LoginLogoutFormInput loginLogoutFormInput, ResponseModel response)
        {
            Int32 Result = 0;
            try
            {
                var procedure = "";
                var parameters = new DynamicParameters();

                var data = _dbsContext.DailyTimeSheet.Where(r => r.EmployeeId == loginLogoutFormInput.EmployeeId && r.LoginDate == loginLogoutFormInput.LoginDate).FirstOrDefault();
                if (data == null || data.EmployeeId == 0)
                {
                    procedure = "InsertLoginLogout";
                    parameters.Add("EmployeeId ", loginLogoutFormInput.EmployeeId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                    parameters.Add("LoginDate ", loginLogoutFormInput.LoginDate.ToShortDateString(), System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                    parameters.Add("LoginTime ", loginLogoutFormInput.LoginTime.ToShortTimeString(), System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                    parameters.Add("LoginLocation ", loginLogoutFormInput.LoginLocation, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    parameters.Add("Flag", 2, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                    Result = 1;

                }
                else
                {
                    procedure = "UpdateLoginLogout";
                    parameters.Add("EmployeeId ", loginLogoutFormInput.EmployeeId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                    parameters.Add("LogOutDate ", loginLogoutFormInput.LogOutDate, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                    parameters.Add("LogOutTime ", loginLogoutFormInput.LogOutTime, System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                    parameters.Add("LogoutLocation", loginLogoutFormInput.LogoutLocation, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                    parameters.Add("TotalHoursWorked ", loginLogoutFormInput.TotalHoursWorked, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                    parameters.Add("Flag", 2, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                    Result = 2;
                }
                using (var con = _dapperDbContext.CreateConnection())
                {
                    await con.ExecuteAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return Result;
        }
        #endregion
    }
}

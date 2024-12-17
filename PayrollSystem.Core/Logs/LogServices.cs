
using Dapper;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Logs;
using System.Reflection;
namespace PayrollSystem.Core.Logs
{
    public class LogServices : ILogServices
    {
        private readonly DapperDbContext _dapperDbContext ;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LogServices( DapperDbContext dapperDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _dapperDbContext = dapperDbContext;
        }


        public async Task InsertExceptionLogs(string ClassName, string MethodName, string Exception, string SiteName)
        {
            try
            {
                var procedure = "InsertExceptionLogs";
                var parameters = new DynamicParameters();
                parameters.Add("ClassName", ClassName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("ActionName", MethodName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Exception", Exception, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("SiteName", SiteName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("CreatedDateTime",DateTime.Now,System.Data.DbType.DateTimeOffset,System.Data.ParameterDirection.Input);

                using (var con=_dapperDbContext.CreateConnection())
                {
                    await con.QueryMultipleAsync(procedure,parameters,commandType:System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task InsertUiExceptionLog(UiExceptionLogInput uiExceptionLogInput, ResponseModel response)
        {
            try
            {
                var procedure = "InsertExceptionLogs";
                var parameters = new DynamicParameters();
                parameters.Add("ClassName", uiExceptionLogInput.ClassName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("ActionName", uiExceptionLogInput.MethodName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Exception", uiExceptionLogInput.Exception, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("SiteName", uiExceptionLogInput.SiteName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("CreatedDateTime", DateTime.Now, System.Data.DbType.DateTimeOffset, System.Data.ParameterDirection.Input);

                using (var con = _dapperDbContext.CreateConnection())
                {
                    await con.QueryMultipleAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                await InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return;
        }

        #region SaveUserLogs
        public async Task InsertUserLogs(UserLogInput userLogInput, ResponseModel response)
        {
            try
            {
                var procedure = "InsertUserLogs";
                var parameters=new DynamicParameters();
                parameters.Add("UserId", userLogInput.UserId ,System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("BrowswerUsed", userLogInput.BrowswerUsed ,System.Data.DbType.String,System.Data.ParameterDirection.Input);
                parameters.Add("IdAddress", userLogInput.IdAddress,System.Data.DbType.String,System.Data.ParameterDirection.Input);
                parameters.Add("Flag",userLogInput.Flag,System.Data.DbType.Int64,System.Data.ParameterDirection.Input);

                using (var con=_dapperDbContext.CreateConnection())
                {
                    await con.ExecuteAsync(procedure,parameters,commandType:System.Data.CommandType.StoredProcedure);
                }

            }
            catch (Exception ex)
            {
                await InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.Message += ex.Message;
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
            }
        }
        #endregion
    }
}

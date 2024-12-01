
using Dapper;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;

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
                Console.WriteLine(ex);
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace PayrollSystem.Data.Common
{
    public class DapperDbContext:DbContext
    {
        private readonly string _ConnectionString;
        public DapperDbContext(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_ConnectionString);

    }
}

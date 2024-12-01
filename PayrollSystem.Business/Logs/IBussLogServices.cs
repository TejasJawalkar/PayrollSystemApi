using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Business.Logs
{
    public interface IBussLogServices
    {
        public Task InsertUserLogs(ResponseModel response);
    }
}

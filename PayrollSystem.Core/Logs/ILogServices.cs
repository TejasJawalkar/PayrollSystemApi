using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Core.Logs
{
    public interface ILogServices
    {
        public Task InsertExceptionLogs(String ClassName,String MethodName,String Exception,String SiteName);
    }
}

using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Logs;

namespace PayrollSystem.Core.Logs
{
    public interface ILogServices
    {
        public Task InsertExceptionLogs(string ClassName, string MethodName, string Exception, string SiteName);
        public Task InsertUserLogs(UserLogInput userLogInput, ResponseModel response);

        public Task InsertUiExceptionLog(UiExceptionLogInput uiExceptionLogInput,ResponseModel response);
    }
}

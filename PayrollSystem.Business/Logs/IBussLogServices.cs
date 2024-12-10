using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Logs;

namespace PayrollSystem.Business.Logs
{
    public interface IBussLogServices
    {
        public Task InsertUserLogs(UserLogInput userLogInput,ResponseModel response);
        public Task InsertUiExceptionLog(UiExceptionLogInput uiExceptionLogInput, ResponseModel response);
    }
}

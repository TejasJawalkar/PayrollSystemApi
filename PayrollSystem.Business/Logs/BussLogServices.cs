using PayrollSystem.Core.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Logs;

namespace PayrollSystem.Business.Logs
{
    public class BussLogServices : IBussLogServices
    {
        private readonly ILogServices _logServices;
        public BussLogServices(ILogServices logServices)
        {
            _logServices = logServices;
        }

        public async Task InsertUiExceptionLog(UiExceptionLogInput uiExceptionLogInput, ResponseModel response)
        {
            try
            {
                await _logServices.InsertUiExceptionLog(uiExceptionLogInput, response);
            }
            catch (Exception ex)
            {
                response.Message += "Internal Server Error";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }

        public async Task InsertUserLogs(UserLogInput userLogInput, ResponseModel response)
        {
			try
			{
                await _logServices.InsertUserLogs(userLogInput, response);
                if(response.ObjectStatusCode!=Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.Message += "Log Saved";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
                else
                {
                    response.Message += "Log Not Saved";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                }
            }
			catch (Exception ex)
			{
                response.Message += "Internal Server Error";
                response.ObjectStatusCode=Entity.InputOutput.Common.StatusCodes.UnknowError;
			}
        }
    }
}

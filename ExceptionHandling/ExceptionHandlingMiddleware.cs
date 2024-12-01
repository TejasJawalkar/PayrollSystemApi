using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Http;
using PayrollSystem.Core.Logs;

namespace ExceptionHandling
{
    public class ExcetionHandlingMiddleware
    {
        #region Exception Messages
        public const string UnauthorizedAccessException = "You are not authorized user, Please contact site admin";
        public const string InvalidAccessTokenException = "Invalid Token Details";
        #region ExceptionType
        public const string UnauthorizedAccess = "UnauthorizedAccess";
        public const string InvalidAccessToken = "InvalidAccessToken";
        public const string InternalServerError = "InternalServerError";
        #endregion
        #endregion

        #region Private Variables
        private readonly RequestDelegate _next;
        private readonly ILogServices _logService;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next">It will get populated by framework</param>
        public ExcetionHandlingMiddleware(RequestDelegate next, ILogServices logService)
        {
            this._next = next;
            _logService = logService;
        }
        #endregion

        #region Public Methods

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle exception and provide appropriate response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = exception.Message });

            switch (exception.Message)
            {
                case UnauthorizedAccess:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = UnauthorizedAccessException });
                    await _logService.InsertExceptionLogs(
                          Convert.ToString(context.Request.RouteValues["controller"]),
                          Convert.ToString(context.Request.RouteValues["action"]),
                          exception.Message,
                          context.Request.Host.Value.Trim());
                    break;

                case InvalidAccessToken:
                    code = HttpStatusCode.Unauthorized;
                    result = JsonConvert.SerializeObject(new { error = InvalidAccessTokenException });
                    await _logService.InsertExceptionLogs(
                      Convert.ToString(context.Request.RouteValues["controller"]),
                      Convert.ToString(context.Request.RouteValues["action"]),
                      exception.Message,
                      context.Request.Host.Value.Trim());
                    break;

                case InternalServerError:
                    code = HttpStatusCode.InternalServerError;
                    result = JsonConvert.SerializeObject(new { error = exception.InnerException.Message });
                    await _logService.InsertExceptionLogs(
                        Convert.ToString(context.Request.RouteValues["controller"]),
                        Convert.ToString(context.Request.RouteValues["action"]),
                        exception.Message,
                        context.Request.Host.Value.Trim());
                    break;

                default:
                    await _logService.InsertExceptionLogs(
                      Convert.ToString(context.Request.RouteValues["controller"]),
                      Convert.ToString(context.Request.RouteValues["action"]),
                      exception.Message,
                      context.Request.Host.Value.Trim());
                    break;
            }

            if (exception.GetType() == typeof(UnauthorizedAccessException))
            {
                code = HttpStatusCode.Unauthorized;
                result = JsonConvert.SerializeObject(new { error = UnauthorizedAccessException });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
        #endregion
    }
}
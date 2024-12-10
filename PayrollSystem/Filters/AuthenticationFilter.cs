using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Login;
using PayrollSystem.Entity.Models.Employee;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace PayrollSystem.Filters
{
    public class AuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (!GetAllowAnonymousAPIs().Contains(Convert.ToString(context.RouteData.Values["Action"])))
                {
                    var TokenString = context.HttpContext.Request.Headers.Authorization.ToString();
                    ResponseModel response = new ResponseModel();
                    if (string.IsNullOrEmpty(TokenString))
                    {
                        throw new UnauthorizedAccessException(MessageConstants.UnAuthorized_Access);
                    }

                    string Token = TokenString.Split(" ")[1];
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var token1 = tokenHandler.ReadJwtToken(Token);
                    var EmployeeId = token1.Claims.FirstOrDefault(c => c.Type == "EmployeeId")?.Value;

                    if (EmployeeId != null)
                    {
                        var OrgnisationId = token1.Claims.FirstOrDefault(c => c.Type == "OrgnisationId")?.Value;
                        var Role = token1.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
                        var TokenExpiryDate = token1.ValidTo;
                        DateTime UTCTime = System.DateTime.UtcNow;
                        DateTime IndianTime = TokenExpiryDate.AddHours(5.5);
                        TokenExpiryDate = IndianTime;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException(MessageConstants.Invalid_Token_Access);
                    }
                }

            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }

        private List<String> GetAllowAnonymousAPIs()
        {
            List<string> allowedApis = new List<string>();
            allowedApis.Add("EmployeeLogin");
            allowedApis.Add("NewRegister");
            allowedApis.Add("InsertUserLogs");
            allowedApis.Add("InsertExceptionLogs");

            return allowedApis;
        }
    }
}

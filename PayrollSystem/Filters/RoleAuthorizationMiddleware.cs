using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;
using System.Security.Claims;

namespace PayrollSystem.Filters
{
    public class RoleAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        public RoleAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (!optionalRoutes().Contains(Convert.ToString(context.Request.RouteValues["Action"])))
                {
                    var user = context.User;
                        var roles = user.FindAll("Role").Select(r => r.Value).ToList();
                    if (user.Identity.IsAuthenticated)
                    {
                        Console.WriteLine($"User roles: {string.Join(", ", roles)}");
                    }
                    else
                    {
                        Console.WriteLine($"User roles: {string.Join(", ", roles)}");
                        throw new UnauthorizedAccessException();
                    }
                }
                await _next(context);
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException();
            }
        }

        private List<string> optionalRoutes()
        {
            List<string> allowedApis = new List<string>();
            allowedApis.Add("EmployeeLogin");
            allowedApis.Add("NewRegister");
            return allowedApis;
        }
    }
}

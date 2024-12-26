using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Filters
{
    public class ApiKeyAuthenticateFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;

        public ApiKeyAuthenticateFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!GetAllowAnonymousAPIs().Contains(Convert.ToString(context.RouteData.Values["Action"])))
            {
                // Retrieve the API Key from the request headers
                var apiKeyValue = context.HttpContext.Request.Headers["apikeyvalue"].ToString();

                // Check if the API key is provided
                if (string.IsNullOrEmpty(apiKeyValue))
                {
                    var apiResponse = new ApiKeyResponseModel
                    {
                        Message = "No Api Key Found",
                        StatusCode = 1
                    };
                    context.Result = new UnauthorizedObjectResult(apiResponse);
                    return;
                }

                // Retrieve the valid API key from configuration
                var validApiKey = _configuration["ApiSettings:ValidApiKey"];

                // Check if the API key matches the valid key
                if (apiKeyValue != validApiKey)
                {
                    var apiResponse = new ApiKeyResponseModel
                    {
                        Message = "Api Key Not Matched",
                        StatusCode = 2
                    };
                    context.Result = new UnauthorizedObjectResult(apiResponse);
                    return;
                }

                // If the API key matches, proceed to the controller action
                if (apiKeyValue == validApiKey)
                {
                    // Proceed with the request to the controller action
                    context.Result = null; // Allow the request to proceed
                }
            }
        }

        private List<String> GetAllowAnonymousAPIs()
        {
            List<string> allowedApis = new List<string>();
            allowedApis.Add("AddOrganization");
            allowedApis.Add("AddRoles");
            allowedApis.Add("AddDepartments");
            return allowedApis;
        }
    }
}

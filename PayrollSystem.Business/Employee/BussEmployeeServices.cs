
#region
using Azure;
using Microsoft.IdentityModel.Tokens;
using PayrollSystem.Business.Common;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.Employee;
using PayrollSystem.Core.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
#endregion

namespace PayrollSystem.Business.Employee
{
    public class BussEmployeeServices : IBussEmployeeServices
    {
        #region Object Declaration
        private readonly IEmployeeServices _employeeServices;
        private readonly IConfiguration _configuration;
        private readonly ILogServices _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public BussEmployeeServices(IEmployeeServices employeeServices, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogServices logger)
        {
            _employeeServices = employeeServices;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion

        #region EmployeeLogin
        public async Task EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response)
        {
            TokenOutput tokenOutput = new TokenOutput();
            try
            {
                tokenOutput = await _employeeServices.EmployeeLogin(employeeLoginInput, response);
                AuthenticationToken authenticationToken = new AuthenticationToken();
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (tokenOutput.EmployeeId != 0)
                    {
                        authenticationToken.Token = generateAuthenticationToken(tokenOutput);
                        authenticationToken.OrgnisationId = tokenOutput.OrgnisationId;
                        authenticationToken.DepartmentId = tokenOutput.DepartmentId;
                        authenticationToken.EmployeeId = tokenOutput.EmployeeId;
                        authenticationToken.Role = tokenOutput.RoleId;

                        response.Message += "Login Success";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else
                    {
                        response.Message += "Login Failed";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                    }
                }
                response.Data = tokenOutput.EmployeeId == 0 ? null:authenticationToken;
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(this.GetType().Name,
                  Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                  ex.Message,
                  _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion

        #region generateAuthenticationToken
        public string generateAuthenticationToken(TokenOutput tokenOutput)
        {
            string encodetoken = "";
            try
            {
                var tokenClaims = new List<Claim> {
                    new Claim("EmployeeId",tokenOutput.EmployeeId.ToString()),
                    new Claim("OrganisationId",tokenOutput.OrgnisationId.ToString()),
                    new Claim("OrgnisationEmail",tokenOutput.OrgnisationEmail.ToString()),
                    new Claim("RoleId",tokenOutput.RoleId.ToString()),
                    new Claim("DepartmentId",tokenOutput.DepartmentId.ToString()),
                    new Claim("IsActive",tokenOutput.IsActive.ToString()),
                };
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AuthConfig:Secret").Get<String>());

                var JwtToken = new JwtSecurityToken(
                    issuer: _configuration.GetSection("AuthConfig:Issuser").Get<String>(),
                    audience: _configuration.GetSection("AuthConfig:Audience").Get<String>(),
                    claims: tokenClaims,
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddMinutes(30)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                encodetoken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            }
            catch (Exception ex)
            {
                _logger.InsertExceptionLogs(ex.Message,
                   this.GetType().Name,
                   Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]),
                   _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return encodetoken;
        }
        #endregion

        #region  NewRegister
        /// <summary>
        /// NewPasswordRegistration
        /// </summary>
        /// <param name="EmailId"></param>
        /// <param name="Password"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task NewRegister(string EmailId, string Password, ResponseModel response)
        {
            Int32 result;
            try
            {
                result = await _employeeServices.NewRegister(EmailId, Password, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (result == 1)
                    {
                        response.Message += "New Password Registration Success";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else if (result == 3)
                    {
                        response.Message += "New Password Not Allowed";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else
                    {
                        response.Message += "New Password Registration Failed";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                    }
                }
                else
                {
                    response.Message += "New Password Registration Failed";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(ex.Message,
                    this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]),
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion

    }
}

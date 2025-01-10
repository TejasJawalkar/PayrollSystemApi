
#region
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using PayrollSystem.Core.Employee;
using PayrollSystem.Core.Logs;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
using PayrollSystem.Entity.Models.Employee;
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
                response.Data = tokenOutput.EmployeeId == 0 ? null : authenticationToken;
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
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
                _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
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
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
        }
        #endregion

        #region SignInStatus
        /// <summary>
        /// Get Sign In Status for change the button text on ui for sing In or sing Out  
        /// </summary>
        /// <param name="employeeFormInput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task SignInStatus(EmployeeFormInput employeeFormInput, ResponseModel response)
        {
            Int32 result = 0;
            try
            {
                result = await _employeeServices.SignInStatus(employeeFormInput, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (result != 3 || result != 0)
                    {
                        response.Message += "Employee Found for Today";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else
                    {
                        response.Message += "Employee Not Found for Today";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            response.Data = result;
        }
        #endregion

        #region AddUpdateSignInSignOut
        /// <summary>
        /// Add Update Sign In Sign Out
        /// </summary>
        /// <param name="employeeFormInput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task AddUpdateSignInSignOut(LoginLogoutFormInput loginLogoutFormInput, ResponseModel response)
        {
            Int32 result = 0;
            Double TotalHourseWorked = 0;
            try
            {
                TotalHourseWorked = await GetCalculatedTotalHourseofEmployee(loginLogoutFormInput.EmployeeId, loginLogoutFormInput.LoginDate);
                if (TotalHourseWorked != 0)
                {
                    loginLogoutFormInput.TotalHoursWorked = TotalHourseWorked;
                }
                result = await _employeeServices.AddUpdateSignInSignOut(loginLogoutFormInput, response);
                if (result == 1 || result == 2)
                {
                    response.Message += "SignIn and SignOut Inserted/Updated";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
                else
                {
                    response.Message += "Employee Cannot SignIn/SignOut";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.NotExists;
                }
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            response.Data = result;
        }
        #endregion

        #region GetCalculatedTotalHourseofEmployee
        private async Task<Double> GetCalculatedTotalHourseofEmployee(Int64 EmployeeId, DateTime LoginDate)
        {

            DailyTimeSheet dailyTimeSheet = null;
            Double totalhourseWorked = 0;
            try
            {
                dailyTimeSheet = await _employeeServices.TotalHoursWorkedCalculation(EmployeeId, LoginDate);

                if (dailyTimeSheet != null)
                {
                    if (dailyTimeSheet.LoginTime.ToString().Trim() != null && dailyTimeSheet.LogOutTime.ToString().Trim() != null)
                    {
                        totalhourseWorked = Convert.ToDouble((dailyTimeSheet.LoginTime - dailyTimeSheet.LogOutTime));
                    }
                    return totalhourseWorked;
                }
                else
                {
                    return totalhourseWorked;
                }
            }
            catch (Exception ex)
            {
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name, ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                return totalhourseWorked;
            }
        }
        #endregion

        #region AddNewLeave
        /// <summary>
        /// Employee Will Add New Leave
        /// </summary>
        /// <param name="userLeaveInput"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public async Task AddNewLeave(UserLeaveInput userLeaveInput, ResponseModel response)
        {
            Int32 result = 0;
            double TotalDays = 0;
            try
            {
                TotalDays = (userLeaveInput.ToDate - userLeaveInput.FromDate).TotalDays;
                userLeaveInput.NoofDays = TotalDays;
                result = await _employeeServices.AddNewLeave(userLeaveInput, response);
                if (result == 1)
                {
                    response.Message += "Employee Leaves Saved Successfully.";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
                else if (result == 8)
                {
                    response.Message += "Leave Balance is low than the No of Days.";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
                else
                {
                    response.Message += "Employee Leaves Not Saved.";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                }
                response.Data = result;
            }
            catch (Exception ex)
            {
                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name.ToString(), ex.Message.ToString(), Convert.ToString(_httpContextAccessor.HttpContext.Request.Host.Value.Trim()));
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }
        #endregion

        #region GetLeaveStatus
        public async Task GetLeaveStatus(Int64 EmployeeId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            try
            {
                outputList = await _employeeServices.GetLeaveStatus(EmployeeId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (outputList != null)
                    {
                        response.Message += "Employee Leave Status Found";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else
                    {
                        response.Message += "Employee Leave Status Not Found";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                    }
                }
                else
                {
                    response.Message += "Employee Leave Status Not Found";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                }
            }
            catch (Exception ex)
            {

                await _logger.InsertExceptionLogs(Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), this.GetType().Name.ToString(), ex.Message.ToString(), Convert.ToString(_httpContextAccessor.HttpContext.Request.Host.Value.Trim()));
                response.Message += "Internal Server Error, Try Again Later";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }
        #endregion
    }
}

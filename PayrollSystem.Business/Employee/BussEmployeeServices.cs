﻿
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

namespace PayrollSystem.Business.Employee
{
    public class BussEmployeeServices : IBussEmployeeServices, IBussCommonServices
    {
        private readonly IEmployeeServices _employeeServices;
        private readonly IConfiguration _configuration;
        private readonly ILogServices _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommonServices _commonServices;

        public BussEmployeeServices(IEmployeeServices employeeServices, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILogServices logger, ICommonServices commonServices)
        {
            _employeeServices = employeeServices;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _commonServices=commonServices;
        }
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
                        authenticationToken.EmployeeId = tokenOutput.EmployeeId;
                        authenticationToken.Role = tokenOutput.Role;

                        response.Message += "Login Success";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                    }
                    else
                    {
                        response.Message += "Login Failed";
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                    }
                }
                response.Data = authenticationToken;
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

        public string generateAuthenticationToken(TokenOutput tokenOutput)
        {
            string encodetoken = "";
            try
            {
                var tokenClaims = new List<Claim> {
                    new Claim("EmployeeId",tokenOutput.EmployeeId.ToString()),
                    new Claim("OrganisationId",tokenOutput.OrgnisationId.ToString()),
                    new Claim("OrgnisationEmail",tokenOutput.OrgnisationEmail.ToString()),
                    new Claim("Role",tokenOutput.Role.ToString()),
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

        public async Task GetEmployee(long EmployeeId, long OrgnisationId, ResponseModel response)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                employeeDetails = await _commonServices.GetEmployee(EmployeeId, OrgnisationId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.Message += "New Password Registration Success";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
                }
                else
                {
                    response.Message += "New Password Registration Failed";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Error;
                }
                response.Data = employeeDetails;
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

        public async Task NewRegister(string EmailId, string Password, ResponseModel response)
        {
            try
            {
                await _employeeServices.NewRegister(EmailId, Password, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    response.Message += "New Password Registration Success";
                    response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.Success;
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

        public async Task GetAllEmployee(long OrganisationId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            try
            {
                outputList = await _commonServices.GetAllEmployee(OrganisationId, response);
                if (response.ObjectStatusCode != Entity.InputOutput.Common.StatusCodes.UnknowError)
                {
                    if (outputList.EmployeeDetails.Count != 0)
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                        response.Message = "Employee List";

                        response.Data = outputList.EmployeeDetails;
                    }
                    else
                    {
                        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                        response.Message = "No Employee's Found";
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
            }
        }
    }
}
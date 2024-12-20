﻿using Dapper;
using PayrollSystem.Core.Common;
using PayrollSystem.Core.Logs;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.HR;

namespace PayrollSystem.Core.HR
{
    public class HRServices : IHrServices, ICommonServices
    {
        private readonly IConfiguration _configuration;
        private readonly DapperDbContext _dapperDbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogServices _logger;

        public HRServices(IConfiguration configuration, DapperDbContext dapperDbContext, IHttpContextAccessor httpContextAccessor, ILogServices logger)
        {
            _configuration = configuration;
            _dapperDbContext = dapperDbContext;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<OutputList> GetAllEmployee(long OrgnisationId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            outputList.EmployeeDetails = new List<EmployeeDetails>();
            
            try
            {
                var procedure = "GetAllEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrganisationId", OrgnisationId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                using (var con=_dapperDbContext.CreateConnection())
                {
                    var reader = await con.QueryMultipleAsync(procedure,parameters,commandType:System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        outputList.EmployeeDetails= reader.Read<EmployeeDetails>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logger.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Found";
            }
            return outputList;
        }

        public async Task<Int32> RegisterNewEmployee(NewEmployeeInput newEmployee, ResponseModel response)
        {
            Int32 Result=0;
            try
            {
                var procedure = "AddNewEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrgnisationID", newEmployee.OrgnisationID, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("EmployeeName", newEmployee.EmployeeName, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("OrganisationEmail", newEmployee.OrganisationEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("PersonalEmail", newEmployee.PersonalEmail, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("Mobile", newEmployee.OrgnisationID, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                parameters.Add("role", newEmployee.OrgnisationID, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                parameters.Add("IsActive", newEmployee.IsActive, System.Data.DbType.Boolean, System.Data.ParameterDirection.Input);

                using (var con = _dapperDbContext.CreateConnection())
                {
                    Result = await con.QueryFirstOrDefaultAsync<Int32>(procedure,parameters,commandType:System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
               await _logger.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Registered\";";
            }
            return Result;
        }

        public async Task<EmployeeDetails> GetEmployee(Int64 Id, Int64 OrgnisationId, ResponseModel response)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                var procedure = "GetEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrgnisationId", OrgnisationId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("Id", Id, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                using (var con = _dapperDbContext.CreateConnection())
                {
                    employeeDetails = await con.QueryFirstOrDefaultAsync<EmployeeDetails>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logger.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return employeeDetails;
        }
    }
}

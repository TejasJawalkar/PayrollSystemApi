#region Imports
using Dapper;
using Microsoft.EntityFrameworkCore;
using PayrollSystem.Core.Logs;
using PayrollSystem.Data.Common;
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.System;
using System.Data;
#endregion

namespace PayrollSystem.Core.Common
{
    public class CommonTaskServices : ICommonTaskServices
    {
        #region Object Declaration
        private readonly DbsContext _DbsContext;
        private readonly DapperDbContext _DapperDbContext;
        private readonly ILogServices _logServices;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        #region Constructor
        public CommonTaskServices(DbsContext dbsContext, DapperDbContext DapperDbContext, ILogServices logServices, IHttpContextAccessor httpContextAccessor)
        {
            _DapperDbContext = DapperDbContext;
            _DbsContext = dbsContext;
            _logServices = logServices;
            _httpContextAccessor = httpContextAccessor;
        }
        #endregion

        #region GetRoles
        public async Task<OutputList> GetRoles(ResponseModel response)
        {
            OutputList outputList = new OutputList();
            outputList.Roles = new List<RolesOutput>();
            try
            {
                outputList.Roles = _DbsContext.Roles.Select(r => new RolesOutput
                {
                    RoleId = r.RoleId,
                    Role = r.Role,
                    Stamp = r.Stamp,
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }

            return await Task.FromResult(outputList);
        }
        #endregion

        #region GetDepartments

        public async Task<OutputList> GetDepartments(ResponseModel response)
        {
            OutputList outputList = new OutputList();
            outputList.Departments = new List<DepartmentOutput>();
            try
            {
                outputList.Departments = _DbsContext.Departments.Select(r => new DepartmentOutput
                {
                    DepartmentId = r.DepartmentId,
                    DepartementName = r.DepartementName
                }).ToList();
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return await Task.FromResult(outputList);
        }
        #endregion

        #region GetAllEmployee
        public async Task<OutputList> GetAllEmployee(long OrgnisationId, ResponseModel response)
        {
            OutputList outputList = new OutputList();
            outputList.EmployeeDetails = new List<EmployeeDetails>();

            try
            {
                var procedure = "GetAllEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrganisationId", OrgnisationId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                using (var con = _DapperDbContext.CreateConnection())
                {
                    var reader = await con.QueryMultipleAsync(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                    if (reader != null)
                    {
                        outputList.EmployeeDetails = reader.Read<EmployeeDetails>().ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                await _logServices.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Found";
            }
            return outputList;
        }
        #endregion

        #region GetEmployee
        public async Task<EmployeeDetails> GetEmployee(Int64 EmployeeId, Int64 OrgnisationId, ResponseModel response)
        {
            EmployeeDetails employeeDetails = new EmployeeDetails();
            try
            {
                var procedure = "GetEmployee";
                var parameters = new DynamicParameters();
                parameters.Add("OrganisationId", OrgnisationId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                parameters.Add("EmployeeId", EmployeeId, System.Data.DbType.Int64, System.Data.ParameterDirection.Input);
                using (var con = _DapperDbContext.CreateConnection())
                {
                    employeeDetails = await con.QueryFirstOrDefaultAsync<EmployeeDetails>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                response.Message += "Internal server error.Please try again.";
                response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                await _logServices.InsertExceptionLogs(this.GetType().Name, Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["action"]), ex.Message, _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
            }
            return employeeDetails;
        }
        #endregion

        #region GetOrganisationDetails
        public async Task<OutputOrganization> GetOrganisationDetails(long OrganizationId, ResponseModel response)
        {
            OutputOrganization outputOrganization = new OutputOrganization();
            try
            {
                outputOrganization = (from o in _DbsContext.Oragnizations
                                      where o.OrgnisationID == OrganizationId
                                      select new OutputOrganization
                                      {
                                          OrgnisationID = o.OrgnisationID,
                                          OrgnizationName = o.OrgnizationName,
                                          OrgnisationAddress = o.OrgnisationAddress,
                                          OrgnisationCountry = o.OrgnisationCountry,
                                          OrgnisationPincode = o.OrgnisationPincode,
                                          OrgnisationStartDate = o.OrgnisationStartDate,
                                          OrgnisationState = o.OrgnisationState,
                                          OrgnisationDirectorName = o.OrgnisationDirectorName,
                                          DirectorMobileNo = o.DirectorMobileNo,
                                          DirectorEmail = o.DirectorEmail,
                                          OrgnisationCeo = o.OrgnisationCeo,
                                          CeoMobileNo = o.CeoMobileNo,
                                          CeoEmail = o.CeoEmail,
                                          OrgnisationGstNo=o.OrgnisationGstNo,
                                          OrgnisationStartTime=  o.OrgnisationStartTime,
                                          OrgnisationEndTime =o.OrgnisationEndTime,

                                      }).FirstOrDefault();

        }
            catch (Exception ex)
            {

                await _logServices.InsertExceptionLogs(this.GetType().Name,
                    Convert.ToString(_httpContextAccessor.HttpContext.Request.RouteValues["Action"]),
                    ex.Message,
                    _httpContextAccessor.HttpContext.Request.Host.Value.Trim());
        response.ObjectStatusCode = Entity.InputOutput.Common.StatusCodes.UnknowError;
                response.Message += "Employee Not Found";
            }
            return await Task.FromResult(outputOrganization);
}
        #endregion

    }
}

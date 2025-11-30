using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.System;

namespace PayrollSystem.Core.Common
{
    public interface ICommonTaskServices
    {
        public Task<OutputList> GetRoles(ResponseModel response);
        public Task<OutputList> GetDepartments(ResponseModel response);
        public Task<EmployeeDetails> GetEmployee(Int64 Id, Int64 OrgnisationId, ResponseModel response);
        public Task<OutputList> GetAllEmployee(Int64 OrgnisationId, ResponseModel response);
        public Task<OutputOrganization> GetOrganisationDetails(Int64 OrganizationId, ResponseModel response);
    }
}

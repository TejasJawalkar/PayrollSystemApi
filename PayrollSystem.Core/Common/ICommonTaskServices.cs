using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;

namespace PayrollSystem.Core.Common
{
    public interface ICommonTaskServices
    {
        public Task<OutputList> GetRoles(ResponseModel response);
        public Task<OutputList> GetDepartments(ResponseModel response);
        public Task<EmployeeDetails> GetEmployee(Int64 Id, Int64 OrgnisationId, ResponseModel response);
        public Task<OutputList> GetAllEmployee(Int64 OrgnisationId, ResponseModel response);
    }
}

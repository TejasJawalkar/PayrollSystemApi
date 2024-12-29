using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Business.Common
{
    public interface IBussCommonTaskServices
    {
        public Task GetEmployee(Int64 EmployeeId, Int64 OrgnisationId, ResponseModel response);
        public Task GetAllEmployee(Int64 OrganisationId, ResponseModel response);
        public Task GetRoles(ResponseModel response);
        public Task GetDepartments(ResponseModel response);
    }
}

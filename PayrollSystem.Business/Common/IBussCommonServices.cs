using Microsoft.AspNetCore.Mvc;
using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Business.Common
{
    public interface IBussCommonServices
    {
        public Task GetEmployee(Int64 EmployeeId, Int64 OrgnisationId, ResponseModel response);
        public Task GetAllEmployee(Int64 OrganisationId, ResponseModel response);
    }
}

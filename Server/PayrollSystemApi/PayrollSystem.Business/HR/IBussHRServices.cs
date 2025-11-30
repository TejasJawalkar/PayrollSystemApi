using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.HR;

namespace PayrollSystem.Business.HR
{
    public interface IBussHrServices
    {
        public Task RegisterNewEmployee(NewEmployeeInput newEmployee,ResponseModel response);
    }
}

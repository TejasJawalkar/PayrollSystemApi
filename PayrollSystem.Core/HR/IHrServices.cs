using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.HR;

namespace PayrollSystem.Core.HR
{
    public interface IHrServices
    {
        public Task<Int32> RegisterNewEmployee(NewEmployeeInput newEmployee, ResponseModel response);
        
        
    }
}

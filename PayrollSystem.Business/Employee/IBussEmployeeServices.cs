using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;

namespace PayrollSystem.Business.Employee
{
    public interface IBussEmployeeServices
    {
        public Task EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response);
        public Task NewRegister(String EmailId, String Password, ResponseModel response);
    }
}

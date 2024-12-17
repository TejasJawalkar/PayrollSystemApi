using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;

namespace PayrollSystem.Core.Employee
{
    public interface IEmployeeServices
    {
        public Task<TokenOutput> EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response);
        public Task<Int32> NewRegister(String EmailId, String Password, ResponseModel response);
    }
}

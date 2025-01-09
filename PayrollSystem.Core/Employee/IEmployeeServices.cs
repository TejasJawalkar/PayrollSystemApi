using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
using PayrollSystem.Entity.Models.Employee;

namespace PayrollSystem.Core.Employee
{
    public interface IEmployeeServices
    {
        public Task<TokenOutput> EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response);
        public Task<Int32> NewRegister(String EmailId, String Password, ResponseModel response);
        public Task<Int32> SignInStatus(EmployeeFormInput employeeFormInput, ResponseModel response);
        public Task<Int32> AddUpdateSignInSignOut(LoginLogoutFormInput loginLogoutFormInput, ResponseModel response);
        public Task<DailyTimeSheet> TotalHoursWorkedCalculation(Int64 EmployeeId, DateTime LoginDate);
    }
}

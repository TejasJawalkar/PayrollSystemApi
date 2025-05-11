#region Namespace
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
using PayrollSystem.Entity.InputOutput.Login;
using PayrollSystem.Entity.Models.Employee;
#endregion

namespace PayrollSystem.Core.Employee
{
    public interface IEmployeeServices
    {
        #region ConcreteMethods
        public Task<TokenOutput> EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response);
        public Task<Int32> NewRegister(String EmailId, String Password, ResponseModel response);
        public Task<Int32> GetTodaySignInStatus(Int64 EmployeeId, DateTime TodayDate, ResponseModel response);
    }
}

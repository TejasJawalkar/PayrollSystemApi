#region Namespace
using PayrollSystem.Entity.InputOutput.Common;
using PayrollSystem.Entity.InputOutput.Employee;
#endregion

namespace PayrollSystem.Business.Employee
{
    public interface IBussEmployeeServices
    {
        #region ConcreteMethods
        public Task EmployeeLogin(EmployeeLoginInput employeeLoginInput, ResponseModel response);
        public Task NewRegister(String EmailId, String Password, ResponseModel response);
        public Task GetTodaySignInStatus(Int64 EmployeeId, DateTime TodayDate, ResponseModel response);
    }
}

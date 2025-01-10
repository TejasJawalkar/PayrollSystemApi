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
        public Task SignInStatus(EmployeeFormInput employeeFormInput, ResponseModel response);
        public Task AddUpdateSignInSignOut(LoginLogoutFormInput loginLogoutFormInput, ResponseModel response);
        public Task AddNewLeave(UserLeaveInput userLeaveInput, ResponseModel response);
        public Task GetLeaveStatus(Int64 EmployeeId, ResponseModel response);
        #endregion
    }
}

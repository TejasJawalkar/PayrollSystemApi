namespace PayrollSystem.Entity.InputOutput.Login
{
    public class AuthenticationToken
    {
        public string Token { get; set; }
        public Int64 EmployeeId { get; set; }
        public Int64 OrgnisationId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 Role { get; set; }
    }
}

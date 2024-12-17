namespace PayrollSystem.Entity.InputOutput.Login
{
    public class TokenOutput
    {
        public Int64 EmployeeId { get; set; }   
        public Int64 OrgnisationId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 RoleId { get;set; }
        public String OrgnisationEmail { get; set; }
        public bool IsActive { get; set; }  
    }
}
    
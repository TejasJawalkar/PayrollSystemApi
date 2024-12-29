using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Entity.InputOutput.Employee
{
    public class OutputList
    {
        public List<EmployeeDetails> EmployeeDetails { get; set; }
        public List<RolesOutput> Roles { get; set; } 
        public List<DepartmentOutput> Departments { get; set; }
    }
}

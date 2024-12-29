using System.Data;

namespace PayrollSystem.Entity.InputOutput.Common
{
    public class RolesOutput
    {
        public Int64 RoleId { get; set; }
        public String Role { get; set; }
        public Int64 Stamp { get; set; }
    }

    public class DepartmentOutput
    {
        public Int64 DepartmentId { get;  set; }
        public String DepartementName { get; set; }
    }
}

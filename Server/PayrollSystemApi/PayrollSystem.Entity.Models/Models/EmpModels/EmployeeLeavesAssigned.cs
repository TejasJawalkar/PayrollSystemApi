#region Namespace
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace PayrollSystem.Entity.Models.Employee
{
    public class EmployeeLeavesAssigned
    {
        #region Properties
        [Key]
        public Int64 EmployeeLeavesAssignedId { get; set; }
        [AllowNull]
        public Double TotalLeaves { get; set; }
        [AllowNull]
        public Int32 ForYear { get; set; }
        #endregion

        #region References
        public Employee Employee { get; set; }
        #endregion
    }
}

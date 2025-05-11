using PayrollSystem.Entity.InputOutput.Common;

namespace PayrollSystem.Entity.InputOutput.Employee
{
    #region ListClassToMakeSchemaAsList
    public class OutputList
    {
        public List<EmployeeDetails> EmployeeDetails { get; set; }
        public List<RolesOutput> Roles { get; set; }
        public List<DepartmentOutput> Departments { get; set; }
        public List<GetLivesData> GetLivesData { get; set; }
    }
    #endregion

    #region GetLivesData
    public class GetLivesData
    {
        public Int64 LeaveId { get; set; }
        public DateTime AppliedDate { get; set; }
        public Int32 Status { get; set; }
        public string? AppliedReason { get; set; }
        public string? RejectedReason { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Double NoofDays { get; set; }
        public Boolean IsArchived { get; set; }
        public Boolean IsApproved { get; set; }
        public Boolean IsRejected { get; set; }
    }
    #endregion
}

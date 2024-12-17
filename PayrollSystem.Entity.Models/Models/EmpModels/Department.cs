using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Department
    {
        #region Properties
        [Key]
        public Int64 DepartmentId { get; set; }
        [Required]
        [MaxLength(200)]
        public string DepartementName { get; set; }
        #endregion

        #region Relation References
        public Employee Employee { get; set; }
        #endregion
    }
}

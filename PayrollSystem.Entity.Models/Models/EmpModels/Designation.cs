
using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Designation
    {
        #region Properties
        [Key]
        public Int64 RoleId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Role { get; set; }
        #endregion

        #region Relation References
        public Employee Employee { get; set; }
        #endregion
    }
}

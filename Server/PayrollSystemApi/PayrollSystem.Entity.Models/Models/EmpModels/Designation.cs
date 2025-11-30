
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
        [Required]
        [MaxLength(10)]
        public Int64 Stamp { get; set; }
        #endregion

        #region Relation References
        public ICollection<Employee> Employee { get; set; }
        #endregion
    }
}

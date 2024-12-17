using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Entity.Models.Employee
{
    public class ReportingManagers
    {
        [Key]
        public Int64 ManagerID { get; set; }
        [Required]
        public Int64 EmployeeId { get; set; }

        #region 
        public Employee Employee { get; set; }
        public ICollection<EmployeeManagers> EmployeeManagers { get; set; }
        #endregion
    }
}

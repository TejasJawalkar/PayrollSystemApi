using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class PaymentData
    {
        #region Properties
        [Key]
        public Int64 PaymentID { get; set; }
        [Required]
        public double CTC { get; set; }
        [Required]
        public double GrossPay { get; set; }
        [Required]
        public double NetPay { get; set; }
        #endregion 

        #region Relation References
        public Employee Employee { get; set; }
        #endregion
    }
}

using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class PaymentData
    {
        [Key]
        public Int64 PaymentID { get; set; }
        public Int64 EmployeeId { get; set; }

        [Required]
        public double CTC { get; set; }

        [Required]
        public double GrossPay { get; set; }

        [Required]
        public double NetPay { get; set; }

        public Employee Employee { get; set; }
    }
}

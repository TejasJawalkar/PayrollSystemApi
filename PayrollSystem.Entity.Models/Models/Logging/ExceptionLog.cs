using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Logging
{
    public class ExceptionLog
    {
        [Key]
        public Int64 ExceptionId { get; set; }
        [Required]
        [MaxLength(150)]
        public String ClassName { get; set; }
        [Required]
        [MaxLength(150)]
        public String ActionName { get; set; }
        [Required]
        [MaxLength(500)]
        public String ExceptionMessage { get; set; }
        [Required]
        [MaxLength(500)]
        public String SiteName { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }

    }
}

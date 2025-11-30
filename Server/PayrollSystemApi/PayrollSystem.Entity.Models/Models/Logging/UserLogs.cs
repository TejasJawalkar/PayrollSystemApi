using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Logging
{
    public class UserLogs
    {
        [Key]
        public Int64 LogID { get; set; }
        [Required]
        public Int64 UserId { get; set; }
        [Required]
        [MaxLength(200)]
        public String BrowswerUsed { get; set; }
        [Required]
        [MaxLength(100)]
        public String IdAddress { get; set; }
        [Required]
        [MaxLength(500)]
        public String Comment { get; set; }
    }
}

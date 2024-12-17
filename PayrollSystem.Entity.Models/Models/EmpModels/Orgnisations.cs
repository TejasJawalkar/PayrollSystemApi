using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Employee
{
    public class Orgnisations
    {
        [Key]
        public Int64 OrgnisationID { get; set; }
        [Required]
        public string OrgnizationName { get; set; }
        [Required]
        public string OrgnisationAddress { get; set; }
        [Required]
        public string OrgnisationCountry { get; set; }
        [Required]
        public string OrgnisationState { get; set; }
        [Required]
        public string OrgnisationPincode { get; set; }
        [Required]
        public DateTime OrgnisationStartDate { get; set; }
        [Required]
        public string OrgnisationDirectorName { get; set; }
        [Required]
        [Phone]
        public string DirectorMobileNo { get; set; }
        [Required]
        [EmailAddress]
        public string DirectorEmail { get; set; }
        [Required]
        public string OrgnisationCeo { get; set; }
        [Required]
        [Phone]
        public string CeoMobileNo { get; set; }
        [Required]
        [EmailAddress]
        public string CeoEmail { get; set; }
        [Required]
        public string OrgnisationGstNo { get; set; }
        [Required]
        public string OrgnisationStartTime { get; set; }
        [Required]
        public string OrgnisationEndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime SystemRegisteredDate { get; set; } = DateTime.Now;

        #region Reference
        public Employee Employee { get; set; }
        #endregion
    }

}

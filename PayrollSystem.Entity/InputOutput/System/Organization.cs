namespace PayrollSystem.Entity.InputOutput.System
{
    public class InputOrganization
    {
        public string OrgnizationName { get; set; }
        public string OrgnisationAddress { get; set; }
        public string OrgnisationCountry { get; set; }
        public string OrgnisationState { get; set; }
        public string OrgnisationPincode { get; set; }
        public DateTime OrgnisationStartDate { get; set; }
        public string OrgnisationDirectorName { get; set; }
        public string DirectorMobileNo { get; set; }
        public string DirectorEmail { get; set; }
        public string OrgnisationCeo { get; set; }
        public string CeoMobileNo { get; set; }
        public string CeoEmail { get; set; }
        public string OrgnisationGstNo { get; set; }
        public TimeSpan OrgnisationStartTime { get; set; }
        public TimeSpan OrgnisationEndTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime SystemRegisteredDate { get; set; }
    }
}

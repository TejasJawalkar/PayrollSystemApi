using System.ComponentModel.DataAnnotations;

namespace PayrollSystem.Entity.Models.Models.SystemConfigurationModel
{
    public class RoutingNavigationChildModel
    {
        [Key]
        public Int64 ChildRouteID { get; set; }
        public Int64 MainRouteId { get; set; }
        public string RouteName { get; set; }
        public string IconString { get; set; }
        public string RouteUrl { get; set; }

        #region Relation References
        public RoutingNavigationModel RoutingNavigationModel { get; set; }
        #endregion
    }
}

namespace Api.WebApi.Areas.HelpPage
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Api.WebApi.Areas.HelpPage.App_Start;

    /// <summary>
    /// The help page area registration.
    /// </summary>
    public class HelpPageAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// Gets the area name.
        /// </summary>
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        /// <summary>
        /// The register area.
        /// </summary>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default",
                "Help/{action}/{apiId}",
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}
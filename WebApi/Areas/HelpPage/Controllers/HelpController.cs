namespace Api.WebApi.Areas.HelpPage.Controllers
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Api.WebApi.Areas.HelpPage.ModelDescriptions;
    using Api.WebApi.Areas.HelpPage.Models;

    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        /// <summary>
        /// The error view name.
        /// </summary>
        private const string ErrorViewName = "Error";

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpController"/> class. 
        /// </summary>
        public HelpController()
            : this(GlobalConfiguration.Configuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpController"/> class.
        /// </summary>
        public HelpController(HttpConfiguration config)
        {
            this.Configuration = config;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public HttpConfiguration Configuration { get; private set; }

        /// <summary>
        /// The index.
        /// </summary>
        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = this.Configuration.Services.GetDocumentationProvider();
            return this.View(this.Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        /// <summary>
        /// The api.
        /// </summary>
        public ActionResult Api(string apiId)
        {
            if (!string.IsNullOrEmpty(apiId))
            {
                HelpPageApiModel apiModel = this.Configuration.GetHelpPageApiModel(apiId);
                if (apiModel != null)
                {
                    return this.View(apiModel);
                }
            }

            return this.View(ErrorViewName);
        }

        /// <summary>
        /// The resource model.
        /// </summary>
        public ActionResult ResourceModel(string modelName)
        {
            if (!string.IsNullOrEmpty(modelName))
            {
                ModelDescriptionGenerator modelDescriptionGenerator = this.Configuration.GetModelDescriptionGenerator();
                ModelDescription modelDescription;
                if (modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription))
                {
                    return this.View(modelDescription);
                }
            }

            return this.View(ErrorViewName);
        }
    }
}
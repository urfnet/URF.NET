using System;
using System.Web.Http;
using System.Web.Mvc;
using Northwind.Web.Areas.HelpPage.ModelDescriptions;

namespace Northwind.Web.Areas.HelpPage.Controllers
{
    /// <summary>
    /// The controller that will handle requests for the help page.
    /// </summary>
    public class HelpController : Controller
    {
        private const string ErrorViewName = "Error";

        public HttpConfiguration Configuration { get; private set; }

        public HelpController()
        {
            Configuration = GlobalConfiguration.Configuration;
        }

        public ActionResult Index()
        {
            ViewBag.DocumentationProvider = Configuration.Services.GetDocumentationProvider();
            return PartialView(Configuration.Services.GetApiExplorer().ApiDescriptions);
        }

        public ActionResult Api(string apiId)
        {
            if (string.IsNullOrEmpty(apiId))
            {
                return PartialView(ErrorViewName);
            }

            var apiModel = Configuration.GetHelpPageApiModel(apiId);

            return apiModel != null ? PartialView(apiModel) : PartialView(ErrorViewName);
        }

        public ActionResult ResourceModel(string modelName)
        {
            if (String.IsNullOrEmpty(modelName))
            {
                return PartialView(ErrorViewName);
            }

            var modelDescriptionGenerator = Configuration.GetModelDescriptionGenerator();

            ModelDescription modelDescription;
            return modelDescriptionGenerator.GeneratedModels.TryGetValue(modelName, out modelDescription) ? PartialView(modelDescription) : PartialView(ErrorViewName);
        }
    }
}
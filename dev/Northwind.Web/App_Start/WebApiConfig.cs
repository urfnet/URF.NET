using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;

namespace Northwind.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //TODO: Development purpose only. Channge for PROD
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // enables attribute routing
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConfig.Register(config);

            // To disable tracing in your application, please comment out or remove the following line of code. For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            //Uncomment when upgraded to Microsoft.AspNet.WebApi.Core 5.0.0 RTM
            //config.EnsureInitialized();


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            GlobalConfiguration.Configuration.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}
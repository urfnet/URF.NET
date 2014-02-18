#region

using System.Web.Http;
using System.Web.Http.OData.Builder;
using Northwind.Data.Models;

#endregion

namespace Northwind.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();

            var customerEntitySetConfiguration =
                modelBuilder.EntitySet<Customer>("Customer");

            customerEntitySetConfiguration.EntityType.Ignore(t => t.Orders);
            customerEntitySetConfiguration.EntityType.Ignore(t => t.CustomerDemographics);

            var model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{id}",
                new {id = RouteParameter.Optional});
        }
    }
}
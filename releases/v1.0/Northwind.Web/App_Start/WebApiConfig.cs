using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using Northwind.Entity.Models;

namespace Northwind.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();
            var entitySetConfiguration = modelBuilder.EntitySet<Product>("Product");
            entitySetConfiguration.EntityType.Ignore(t => t.Order_Details);
            entitySetConfiguration.EntityType.Ignore(t => t.Category);
            entitySetConfiguration.EntityType.Ignore(t => t.Supplier);

            var model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();
        }
    }
}

#region

using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using Microsoft.Data.Edm;
using Northwind.Data.Models;

#endregion

namespace Northwind.Web
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Add $format support
            //config.MessageHandlers.Add(new FormatQueryMessageHandler());

            // Add NavigationRoutingConvention2 to support POST, PUT, PATCH and DELETE on navigation property
            var conventions = ODataRoutingConventions.CreateDefault();

            var modelBuilder = new ODataConventionModelBuilder();

            var entitySetConfiguration = modelBuilder.EntitySet<Product>("Product");
            entitySetConfiguration.EntityType.Ignore(t => t.OrderDetails);
            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Supplier>("Supplier");

            if (conventions != null)
                config.Routes.MapODataRoute("OData", "odata", modelBuilder.GetEdmModel(), new DefaultODataPathHandler(), conventions);

            // Enable queryable support and allow $format query
            config.EnableQuerySupport(new QueryableAttribute {AllowedQueryOptions = AllowedQueryOptions.Supported | AllowedQueryOptions.Format});
        }
    }
}
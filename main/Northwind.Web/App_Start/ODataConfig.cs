#region

using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using Microsoft.Data.Edm;
using Northwind.Data.Models;
using Northwind.Web.Areas.Spa.Extensions;

#endregion

namespace Northwind.Web
{
    public static class ODataConfig
    {
        static ODataConfig()
        {
            Model = GetModel();
        }

        public static IEdmModel Model { get; private set; }

        public static void Register(HttpConfiguration config)
        {
            // Add $format support
            config.MessageHandlers.Add(new FormatQueryMessageHandler());

            // Add NavigationRoutingConvention2 to support POST, PUT, PATCH and DELETE on navigation property
            var conventions = ODataRoutingConventions.CreateDefault();

            if (conventions != null)
                config.Routes.MapODataRoute("OData", "odata", Model, new DefaultODataPathHandler(), conventions);

            // Enable queryable support and allow $format query
            config.EnableQuerySupport(new QueryableAttribute {AllowedQueryOptions = AllowedQueryOptions.Supported | AllowedQueryOptions.Format});

            // To disable tracing in your application, please comment out or remove the following line of code. For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            config.Filters.Add(new ModelValidationFilterAttribute());
        }

        private static IEdmModel GetModel()
        {
            var modelBuilder = new ODataConventionModelBuilder();

            //The action names, and the parameter names all matter.
            // OData controller and action selection work a little differently than they do in Web API.
            // Instead of being based on route parameters, OData controller and action selection is based on the OData meaning of the request URI.
            // So for example if you made a request for http://server/vroot/$metadata, the request would actually get dispatched to a separate special controller that returns the metadata document for the OData service.
            // The controller name also matches the entity set name (AlbumController).

            var entitySetConfiguration = modelBuilder.EntitySet<Product>("Product"); //<ControllerName>Controller
            entitySetConfiguration.EntityType.Ignore(t => t.OrderDetails);
            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Supplier>("Supplier");

            return modelBuilder.GetEdmModel();
        }
    }
}
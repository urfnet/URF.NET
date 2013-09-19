using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using Microsoft.Data.Edm;
using Northwind.Entity.Models;
using Northwind.Web.Areas.Spa.Extensions;

// ReSharper disable once CheckNamespace
namespace Northwind.Web
{
    public static class ODataConfig
    {
        public static IEdmModel Model { get; private set; }

        static ODataConfig()
        {
            Model = GetModel();
        }

        public static void Register(HttpConfiguration config)
        {
            // Add $format support
            config.MessageHandlers.Add(new FormatQueryMessageHandler());

            // Add NavigationRoutingConvention2 to support POST, PUT, PATCH and DELETE on navigation property
            var conventions = ODataRoutingConventions.CreateDefault();

            if (conventions != null)
            {
                conventions.Insert(0, new CustomNavigationRoutingConvention());

                //OData
                //1 - Creating your EDM model. Model is initialized in constructur by GetModel()
                //2 - Configuring an OData route

                // Enables OData support by adding an OData route and enabling querying support for OData.
                // Action selector and odata media type formatters will be registered in per-controller configuration only
                //config.Routes.MapODataRoute("OData", "odata", Model);
                //config.Routes.MapODataRoute("OData", null, ModelBuilder.GetEdmModel(), new DefaultODataPathHandler(), conventions);
                config.Routes.MapODataRoute("OData", "odata", Model, new DefaultODataPathHandler(), conventions);
            }

            // Enable queryable support and allow $format query
            config.EnableQuerySupport(new QueryableAttribute { AllowedQueryOptions = AllowedQueryOptions.Supported | AllowedQueryOptions.Format });

            // To disable tracing in your application, please comment out or remove the following line of code. For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();

            config.Filters.Add(new ModelValidationFilterAttribute());
        }

        private static IEdmModel GetModel()
        {
            //OData
            //1 - Creating EDM model
            var modelBuilder = new ODataConventionModelBuilder();

            //The action names, and the parameter names all matter.
            // OData controller and action selection work a little differently than they do in Web API.
            // Instead of being based on route parameters, OData controller and action selection is based on the OData meaning of the request URI.
            // So for example if you made a request for http://server/vroot/$metadata, the request would actually get dispatched to a separate special controller that returns the metadata document for the OData service.
            // The controller name also matches the entity set name (AlbumController).

            var entitySetConfiguration = modelBuilder.EntitySet<Product>("ProductAsync"); //<ControllerName>Controller
            //var entitySetConfiguration = modelBuilder.EntitySet<Product>("Product"); //<ControllerName>Controller

            //entitySetConfiguration.EntityType.Ignore(t => t.Category);
            //entitySetConfiguration.EntityType.Ignore(t => t.Supplier);
            entitySetConfiguration.EntityType.Ignore(t => t.Order_Details);

            modelBuilder.EntitySet<Category>("Category");
            modelBuilder.EntitySet<Supplier>("Supplier");

            return modelBuilder.GetEdmModel();
        }
    }
}
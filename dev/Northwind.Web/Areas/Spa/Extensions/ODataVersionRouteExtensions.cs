using System.Collections.Generic;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using System.Web.Http.Routing;
using Microsoft.Data.Edm;
using Northwind.Web.Areas.Spa.Extensions;

// ReSharper disable once CheckNamespace
namespace System.Web.Http
{
    public static class ODataVersionRouteExtensions
    {
        /// <summary>
        ///     Map odata route with query string or header constraints
        /// </summary>
        public static void MapODataRoute(this HttpRouteCollection routes, string routeName, string routePrefix, IEdmModel model, object queryConstraints, object headerConstraints)
        {
            MapODataRoute( routes, routeName, routePrefix, model, new DefaultODataPathHandler(), ODataRoutingConventions.CreateDefault(), queryConstraints, headerConstraints);
        }

        /// <summary>
        ///     Map odata route with query string or header constraints
        /// </summary>
        public static void MapODataRoute(this HttpRouteCollection routes, string routeName, string routePrefix, IEdmModel model, IODataPathHandler pathHandler, IEnumerable<IODataRoutingConvention> routingConventions, object queryConstraints, object headerConstraints)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }

            string routeTemplate = string.IsNullOrEmpty(routePrefix) ? ODataRouteConstants.ODataPathTemplate : (routePrefix + "/" + ODataRouteConstants.ODataPathTemplate);
            var routeConstraint = new ODataVersionRouteConstraint(pathHandler, model, routeName, routingConventions, queryConstraints, headerConstraints);
            var constraints = new HttpRouteValueDictionary {{ODataRouteConstants.ConstraintName, routeConstraint}};

            routes.MapHttpRoute( routeName, routeTemplate, null, constraints);
        }
    }
}
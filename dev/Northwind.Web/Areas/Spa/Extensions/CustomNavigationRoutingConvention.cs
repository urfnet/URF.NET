using System;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using Microsoft.Data.Edm;

namespace Northwind.Web.Areas.Spa.Extensions
{
    /// <summary>
    ///     Custom convention to support GET/POST/PUT/PATCH/DELETE routing on navigation property
    /// </summary>
    public class CustomNavigationRoutingConvention : EntitySetRoutingConvention
    {
        public override string SelectAction(ODataPath odataPath, HttpControllerContext controllerContext, ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (odataPath == null)
            {
                throw new ArgumentNullException("odataPath");
            }

            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (actionMap == null)
            {
                throw new ArgumentNullException("actionMap");
            }

            string prefix = GetHttpPrefix(controllerContext.Request.Method.ToString());

            if (string.IsNullOrEmpty(prefix))
            {
                return null;
            }

            if (odataPath.PathTemplate == "~/entityset/key/navigation" || (odataPath.PathTemplate == "~/entityset/key/cast/navigation"))
            {
                var segment = odataPath.Segments.Last() as NavigationPathSegment;

                if (segment != null)
                {
                    var navigationProperty = segment.NavigationProperty;
                    var declaringType = navigationProperty.DeclaringType as IEdmEntityType;

                    if (declaringType != null)
                    {
                        var keySegment = odataPath.Segments[1] as KeyValuePathSegment;

                        if (keySegment != null)
                        {
                            controllerContext.RouteData.Values[ODataRouteConstants.Key] = keySegment.Value;
                        }

                        string actionName = prefix + navigationProperty.Name + "From" + declaringType.Name;

                        return (actionMap.Contains(actionName) ? actionName : (prefix + navigationProperty.Name));
                    }
                }
            }
            return null;
        }

        public static string GetHttpPrefix(string httpMethod)
        {
            switch (httpMethod)
            {
                case "GET":
                    return "Get";

                case "POST":
                    return "Post";

                case "PUT":
                    return "Put";

                case "MERGE":
                case "PATCH":
                    return "Patch";

                case "DELETE":
                    return "Delete";

                default:
                    return null;
            }
        }
    }
}
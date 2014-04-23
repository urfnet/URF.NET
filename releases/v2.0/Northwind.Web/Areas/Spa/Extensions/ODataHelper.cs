using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData.Routing;
using System.Web.Http.Routing;
using Microsoft.Data.OData;
using Microsoft.Data.OData.Query;

namespace Northwind.Web.Areas.Spa.Extensions
{
    /// <summary>
    ///     Helper class to facilitate building an odata service.
    /// </summary>
    public static class ODataHelper
    {
        /// <summary>
        ///     Helper method to get the odata path for an arbitrary odata uri.
        /// </summary>
        /// <param name="request">The request instance in current context</param>
        /// <param name="uri">OData uri</param>
        /// <returns>The parsed odata path</returns>
        public static ODataPath CreateODataPath(this HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            var newRequest = new HttpRequestMessage(HttpMethod.Get, uri);
            var route = request.GetRouteData().Route;
            var newRoute = new HttpRoute(route.RouteTemplate, new HttpRouteValueDictionary(route.Defaults), new HttpRouteValueDictionary(route.Constraints), new HttpRouteValueDictionary(route.DataTokens), route.Handler);
            var routeData = newRoute.GetRouteData(request.GetConfiguration().VirtualPathRoot, newRequest);
            
            if (routeData == null)
            {
                throw new InvalidOperationException("The link is not a valid odata link.");
            }
            return newRequest.GetODataPath();
        }

        /// <summary>
        ///     Helper method to get the key value from a uri.
        ///     Usually used by $link action to extract the key value from the url in body.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="request">The request instance in current context</param>
        /// <param name="uri">OData uri that contains the key value</param>
        /// <returns>The key value</returns>
        public static TKey GetKeyValue<TKey>(this HttpRequestMessage request, Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }

            //get the odata path Ex: ~/entityset/key/$links/navigation
            var odataPath = request.CreateODataPath(uri);
            var keySegment = odataPath.Segments.OfType<KeyValuePathSegment>().FirstOrDefault();

            if (keySegment == null)
            {
                throw new InvalidOperationException("The link does not contain a key.");
            }
            return (TKey)ODataUriUtils.ConvertFromUriLiteral(keySegment.Value, ODataVersion.V3);
        }

        /// <summary>
        ///     Convert model state errors into string value.
        /// </summary>
        /// <param name="modelState">Model state</param>
        /// <returns>String value which contains all model errors</returns>
        public static string GetModelStateErrorInformation(ModelStateDictionary modelState)
        {
            var errorMessageBuilder = new StringBuilder();

            errorMessageBuilder.AppendLine("Invalid request received.");
            if (modelState == null)
            {
                return errorMessageBuilder.ToString();
            }
            foreach (var key in modelState.Keys.Where(key => modelState[key].Errors.Count > 0))
            {
                errorMessageBuilder.AppendLine(key + ":" + ((modelState[key].Value != null) ? modelState[key].Value.RawValue : "null"));
            }
            return errorMessageBuilder.ToString();
        }
    }
}
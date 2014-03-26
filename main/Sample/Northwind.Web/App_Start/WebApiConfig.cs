#region

using System.Web.Http;

#endregion

namespace Northwind.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{action}/{id}",
                new {id = RouteParameter.Optional});
        }
    }
}
#region

using System.Web.Http;
using System.Web.Http.OData.Builder;
using Northwind.Entities.Models;

#endregion

namespace Northwind.Web.App_Start
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder modelBuilder = new ODataConventionModelBuilder();

            modelBuilder.EntitySet<Customer>(typeof (Customer).Name);
            modelBuilder.EntitySet<Order>(typeof (Order).Name);
            modelBuilder.EntitySet<OrderDetail>(typeof (OrderDetail).Name);
            modelBuilder.EntitySet<CustomerDemographic>(typeof(CustomerDemographic).Name);

            var model = modelBuilder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();
        }
    }
}
#region

using System.Web.Http;
using System.Web.Http.OData.Builder;
using Northwind.Entities.Models;

#endregion

namespace Northwind.Web
{
    public static class ODataConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Customer>(typeof (Customer).Name);
            builder.EntitySet<Order>(typeof (Order).Name);
            builder.EntitySet<OrderDetail>(typeof (OrderDetail).Name);
            builder.EntitySet<CustomerDemographic>(typeof (CustomerDemographic).Name);

            builder.EntitySet<Product>(typeof(Product).Name);
            builder.EntitySet<Category>(typeof(Category).Name); 
            builder.EntitySet<Supplier>(typeof(Supplier).Name); 

            var model = builder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();
        }
    }
}
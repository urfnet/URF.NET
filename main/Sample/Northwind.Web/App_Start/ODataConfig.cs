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
            
            var orderDetailBuilder = builder.EntitySet<OrderDetail>(typeof (OrderDetail).Name);
            orderDetailBuilder.EntityType.HasKey(x => x.ProductID);

            var customerDemographicBuilder = builder.EntitySet<CustomerDemographic>(typeof (CustomerDemographic).Name);
            customerDemographicBuilder.EntityType.HasKey(x => x.CustomerDesc);

            var productBuilder = builder.EntitySet<Product>(typeof(Product).Name);
            productBuilder.EntityType.HasKey(t => t.ProductID);

            builder.EntitySet<Category>(typeof(Category).Name); 
            builder.EntitySet<Supplier>(typeof(Supplier).Name); 

            var model = builder.GetEdmModel();
            config.Routes.MapODataRoute("ODataRoute", "odata", model);

            config.EnableQuerySupport();
        }
    }
}
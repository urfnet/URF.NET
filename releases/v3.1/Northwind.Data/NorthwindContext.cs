#region

using System.Data.Entity;
using Northwind.Data.Mapping;
using Northwind.Data.Models.Mapping;
using Repository.Pattern.Ef6;

#endregion

namespace Northwind.Data.Models
{
    public class NorthwindContext : DataContext
    {
        static NorthwindContext()
        {
            Database.SetInitializer<NorthwindContext>(null);
        }

        public NorthwindContext()
            : base("Name=NorthwindContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new CustomerDemographicMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new ShipperMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new TerritoryMap());
            modelBuilder.Configurations.Add(new Alphabetical_list_of_productMap());
            modelBuilder.Configurations.Add(new AlphabeticalListOfProductMap());
            modelBuilder.Configurations.Add(new Current_Product_ListMap());
            modelBuilder.Configurations.Add(new CurrentProductListMap());
            modelBuilder.Configurations.Add(new Customer_and_Suppliers_by_CityMap());
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityMap());
            modelBuilder.Configurations.Add(new OrderDetailsExtendedMap());
            modelBuilder.Configurations.Add(new Orders_QryMap());
            modelBuilder.Configurations.Add(new OrdersQryMap());
            modelBuilder.Configurations.Add(new OrderSubtotalMap());
            modelBuilder.Configurations.Add(new Products_Above_Average_PriceMap());
            modelBuilder.Configurations.Add(new Products_by_CategoryMap());
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceMap());
            modelBuilder.Configurations.Add(new ProductSalesFor1997Map());
            modelBuilder.Configurations.Add(new ProductsByCategoryMap());
            modelBuilder.Configurations.Add(new SalesTotalsByAmountMap());
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterMap());
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearMap());
        }
    }
}
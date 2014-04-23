#region

using System.Data.Entity;
using Northwind.Data.Mapping;
using Repository;

#endregion

namespace Northwind.Data
{
    public partial class NorthwindContext : DbContextBase, IDbContext
    {
        public NorthwindContext() :
            base("NorthwindContext")
        {
            Database.SetInitializer<NorthwindContext>(null);
            Configuration.ProxyCreationEnabled = false;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
            modelBuilder.Configurations.Add(new AlphabeticalListOfProductMap());
            modelBuilder.Configurations.Add(new CategorySalesFor1997Map());
            modelBuilder.Configurations.Add(new CurrentProductListMap());
            modelBuilder.Configurations.Add(new CustomerAndSuppliersByCityMap());
            modelBuilder.Configurations.Add(new InvoiceMap());
            modelBuilder.Configurations.Add(new OrderDetailsExtendedMap());
            modelBuilder.Configurations.Add(new OrderSubtotalMap());
            modelBuilder.Configurations.Add(new OrdersQryMap());
            modelBuilder.Configurations.Add(new ProductSalesFor1997Map());
            modelBuilder.Configurations.Add(new ProductsAboveAveragePriceMap());
            modelBuilder.Configurations.Add(new ProductsByCategoryMap());
            modelBuilder.Configurations.Add(new SalesByCategoryMap());
            modelBuilder.Configurations.Add(new SalesTotalsByAmountMap());
            modelBuilder.Configurations.Add(new SummaryOfSalesByQuarterMap());
            modelBuilder.Configurations.Add(new SummaryOfSalesByYearMap());
        }
    }
}
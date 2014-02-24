#region

using Northwind.Entitiy.Models;
using Repository.Pattern.Ef6;
using Repository.Providers.EntityFramework.Fakes;

#endregion

namespace Northwind.Test.Fake
{
    public class NorthwindFakeContext : FakeDbContext
    {
        public NorthwindFakeContext()
        {
            AddFakeDbSet<Category, CategoryDbSet>();
            AddFakeDbSet<Customer, CustomerDbSet>();
            AddFakeDbSet<Employee, EmployeeDbSet>();
            AddFakeDbSet<Order, OrderDbSet>();
            AddFakeDbSet<OrderDetail, OrderDetailDbSet>();
            AddFakeDbSet<Supplier, SupplierDbSet>();
            AddFakeDbSet<Product, ProductDbSet>();
            AddFakeDbSet<Region, RegionDbSet>();
            AddFakeDbSet<Shipper, ShippperDbSet>();
            AddFakeDbSet<Territory, TerritoryDbSet>();
        }
    }
}

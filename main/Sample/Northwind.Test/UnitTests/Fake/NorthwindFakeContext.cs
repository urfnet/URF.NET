using Northwind.Entities.Models;

namespace Northwind.Test.UnitTests.Fake
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

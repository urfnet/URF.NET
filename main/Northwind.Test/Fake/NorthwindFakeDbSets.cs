#region

using System.Linq;
using Northwind.Entity.Models;
using Repository.Providers.EntityFramework.Fakes;

#endregion

namespace Northwind.Test.Fake
{
    public class CategoryDbSet : FakeDbSet<Category>
    {
        public override Category Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.CategoryID == (int) keyValues.FirstOrDefault());
        }
    }

    public class CustomerDbSet : FakeDbSet<Customer>
    {
        public override Customer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.CustomerID == (string) keyValues.FirstOrDefault());
        }
    }

    public class EmployeeDbSet : FakeDbSet<Employee>
    {
        public override Employee Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.EmployeeID == (int) keyValues.FirstOrDefault());
        }
    }

    public class OrderDbSet : FakeDbSet<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.OrderID == (int) keyValues.FirstOrDefault());
        }
    }

    public class OrderDetailDbSet : FakeDbSet<OrderDetail>
    {
        public override OrderDetail Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.OrderID == (int) keyValues[0] && t.ProductID == (int) keyValues[1]);
        }
    }

    public class ProductDbSet : FakeDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ProductID == (int) keyValues.FirstOrDefault());
        }
    }

    public class RegionDbSet : FakeDbSet<Region>
    {
        public override Region Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.RegionID == (int) keyValues.FirstOrDefault());
        }
    }

    public class ShippperDbSet : FakeDbSet<Shipper>
    {
        public override Shipper Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ShipperID == (int) keyValues.FirstOrDefault());
        }
    }

    public class TerritoryDbSet : FakeDbSet<Territory>
    {
        public override Territory Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.TerritoryID == (string) keyValues.FirstOrDefault());
        }
    }
}

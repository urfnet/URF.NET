#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Northwind.Entities.Models;
using Repository.Pattern.Ef6;

#endregion

namespace Northwind.Test.Fake
{
    public class CategoryDbSet : FakeDbSet<Category>
    {
        public override Category Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.CategoryID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Category> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Category>(() => Find(keyValues));
        }
    }

    public class CustomerDbSet : FakeDbSet<Customer>
    {
        public override Customer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.CustomerID == (string) keyValues.FirstOrDefault());
        }

        public override Task<Customer> FindAsync(params object[] keyValues)
        {
            return new Task<Customer>(() => Find(keyValues));
        }

        public override Task<Customer> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Customer>(() => Find(keyValues));
        }
    }

    public class EmployeeDbSet : FakeDbSet<Employee>
    {
        public override Employee Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.EmployeeID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Employee> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Employee>(() => this.SingleOrDefault(t => t.EmployeeID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class OrderDbSet : FakeDbSet<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.OrderID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Order> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Order>(() => this.SingleOrDefault(t => t.OrderID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class OrderDetailDbSet : FakeDbSet<OrderDetail>
    {
        public override OrderDetail Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.OrderID == (int) keyValues[0] && t.ProductID == (int) keyValues[1]);
        }

        public override Task<OrderDetail> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<OrderDetail>(() => this.SingleOrDefault(t => t.OrderID == (int) keyValues[0] && t.ProductID == (int) keyValues[1]));
        }
    }

    public class SupplierDbSet : FakeDbSet<Supplier>
    {
        public override Supplier Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.SupplierID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Supplier> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Supplier>(() => this.SingleOrDefault(t => t.SupplierID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class ProductDbSet : FakeDbSet<Product>
    {
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ProductID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Product> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Product>(() => this.SingleOrDefault(t => t.ProductID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class RegionDbSet : FakeDbSet<Region>
    {
        public override Region Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.RegionID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Region> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Region>(() => this.SingleOrDefault(t => t.RegionID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class ShippperDbSet : FakeDbSet<Shipper>
    {
        public override Shipper Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.ShipperID == (int) keyValues.FirstOrDefault());
        }

        public override Task<Shipper> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Shipper>(() => this.SingleOrDefault(t => t.ShipperID == (int) keyValues.FirstOrDefault()));
        }
    }

    public class TerritoryDbSet : FakeDbSet<Territory>
    {
        public override Territory Find(params object[] keyValues)
        {
            return this.SingleOrDefault(t => t.TerritoryID == (string) keyValues.FirstOrDefault());
        }

        public override Task<Territory> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return new Task<Territory>(() => this.SingleOrDefault(t => t.TerritoryID == (string) keyValues.FirstOrDefault()));
        }
    }
}
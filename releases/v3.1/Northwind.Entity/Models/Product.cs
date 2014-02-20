#region

using System.Collections.Generic;
using Repository;
using Repository.Pattern.Infrastructure;

#endregion

namespace Northwind.Data.Models
{
    public class Product : EntityBase
    {
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

        public virtual Category Category { get; set; }
        public int? CategoryID { get; set; }
        public bool Discontinued { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public short? ReorderLevel { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int? SupplierID { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
    }
}
#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    [KnownType(typeof(Category))]
    [KnownType(typeof(OrderDetail))]
    [KnownType(typeof(Supplier))]
    public partial class Product : EntityBase
    {
        public Product()
        {
            Order_Details = new List<OrderDetail>();
        }

        public int ProductID { get; set; }
        public string ProductName { get; set; }
        [ForeignKey("Supplier")]
        public Nullable<int> SupplierID { get; set; }
        [ForeignKey("Category")]
        public Nullable<int> CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public Nullable<short> UnitsOnOrder { get; set; }
        public Nullable<short> ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderDetail> Order_Details { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
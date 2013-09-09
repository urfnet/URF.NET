#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class ProductSalesFor1997 : EntityBase
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductSales { get; set; }
    }
}
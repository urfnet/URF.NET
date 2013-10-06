#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class ProductsAboveAveragePrice : EntityBase
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}
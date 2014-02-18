using System;
using System.Collections.Generic;
using Repository;

namespace Northwind.Data.Models
{
    public partial class ProductsAboveAveragePrice : EntityBase
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}

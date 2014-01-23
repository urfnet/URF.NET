using System;
using System.Collections.Generic;

namespace Northwind.Data.Models
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
    }
}

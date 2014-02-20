using System;
using System.Collections.Generic;
using Repository;
using Repository.Pattern.Infrastructure;

namespace Northwind.Data.Models
{
    public partial class ProductSalesFor1997 : EntityBase
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductSales { get; set; }
    }
}

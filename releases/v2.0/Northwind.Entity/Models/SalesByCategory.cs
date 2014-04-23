#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class SalesByCategory : EntityBase
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> ProductSales { get; set; }
    }
}
#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class CategorySalesFor1997 : EntityBase
    {
        public string CategoryName { get; set; }
        public Nullable<decimal> CategorySales { get; set; }
    }
}
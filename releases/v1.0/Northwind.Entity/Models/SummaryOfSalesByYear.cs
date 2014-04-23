#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class SummaryOfSalesByYear : EntityBase
    {
        public Nullable<DateTime> ShippedDate { get; set; }
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}
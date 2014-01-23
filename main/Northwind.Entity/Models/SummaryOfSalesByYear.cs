using System;
using System.Collections.Generic;

namespace Northwind.Data.Models
{
    public partial class SummaryOfSalesByYear
    {
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}

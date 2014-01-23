using System;
using System.Collections.Generic;

namespace Northwind.Data.Models
{
    public partial class OrderDetailsExtended
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }
        public Nullable<decimal> ExtendedPrice { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Northwind.Data.Models
{
    public partial class OrderSubtotal
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}

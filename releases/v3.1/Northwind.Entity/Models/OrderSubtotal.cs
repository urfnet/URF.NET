using System;
using System.Collections.Generic;
using Repository;

namespace Northwind.Data.Models
{
    public partial class OrderSubtotal : EntityBase
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}

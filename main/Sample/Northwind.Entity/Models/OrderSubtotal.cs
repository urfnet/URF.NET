using System;
using System.Collections.Generic;
using Repository.Pattern.Infrastructure;

namespace Northwind.Entities.Models
{
    public partial class OrderSubtotal : Entity
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}

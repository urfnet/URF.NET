#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class OrderSubtotal : EntityBase
    {
        public int OrderID { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
    }
}
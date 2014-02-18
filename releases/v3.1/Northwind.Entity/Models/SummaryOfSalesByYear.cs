#region

using System;
using Repository;

#endregion

namespace Northwind.Data.Models
{
    public class SummaryOfSalesByYear : EntityBase
    {
        public int OrderID { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Northwind.Data.Models
{
    public partial class SalesTotalsByAmount
    {
        public Nullable<decimal> SaleAmount { get; set; }
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}

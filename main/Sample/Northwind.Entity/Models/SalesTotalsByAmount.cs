using System;
using System.Collections.Generic;
using Repository.Pattern.Infrastructure;

namespace Northwind.Entities.Models
{
    public partial class SalesTotalsByAmount : Entity
    {
        public Nullable<decimal> SaleAmount { get; set; }
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}

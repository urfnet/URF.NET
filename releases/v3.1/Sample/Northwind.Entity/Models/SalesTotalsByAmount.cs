using System;
using System.Collections.Generic;
using Repository.Pattern.Infrastructure;

namespace Northwind.Entitiy.Models
{
    public partial class SalesTotalsByAmount : EntityBase
    {
        public Nullable<decimal> SaleAmount { get; set; }
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
    }
}

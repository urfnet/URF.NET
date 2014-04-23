#region

using System;
using Repository;

#endregion

namespace Northwind.Entity.Models
{
    public partial class SalesTotalsByAmount : EntityBase
    {
        public Nullable<decimal> SaleAmount { get; set; }
        public int OrderID { get; set; }
        public string CompanyName { get; set; }
        public Nullable<DateTime> ShippedDate { get; set; }
    }
}
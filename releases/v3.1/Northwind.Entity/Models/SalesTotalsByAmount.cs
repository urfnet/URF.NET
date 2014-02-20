#region

using System;
using Repository;
using Repository.Pattern.Infrastructure;

#endregion

namespace Northwind.Data.Models
{
    public class SalesTotalsByAmount : EntityBase
    {
        public string CompanyName { get; set; }
        public int OrderID { get; set; }
        public decimal? SaleAmount { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}
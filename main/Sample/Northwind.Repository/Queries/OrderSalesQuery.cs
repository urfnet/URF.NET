#region

using System;
using Northwind.Entitiy.Models;
using Repository.Pattern.Repositories;

#endregion

namespace Northwind.Repository.Queries
{
    public class OrderSalesQuery : QueryObject<Order>
    {
        public decimal Amount { get; set; }
        public string Country { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
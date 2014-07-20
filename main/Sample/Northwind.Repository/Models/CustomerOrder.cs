using System;

namespace Northwind.Repository.Models
{
    public class CustomerOrder
    {
        public string CustomerId { get; set; }
        public string ContactName { get; set; }
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
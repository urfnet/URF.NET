using System;
using System.Collections.Generic;
using Repository;
using Repository.Pattern.Infrastructure;

namespace Northwind.Data.Models
{
    public partial class Customer_and_Suppliers_by_City : EntityBase
    {
        public string City { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string Relationship { get; set; }
    }
}

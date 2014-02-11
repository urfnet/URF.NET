using System;
using System.Collections.Generic;
using Northwind.Data.Models;
using Repository;

namespace Northwind.Data.Models
{
    public partial class Shipper : EntityBase
    {
        public Shipper()
        {
            this.Orders = new List<Order>();
        }

        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}

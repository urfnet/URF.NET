using System;
using System.Collections.Generic;
using Repository;

namespace Northwind.Data.Models
{
    public partial class CustomerDemographic : EntityBase
    {
        public CustomerDemographic()
        {
            this.Customers = new List<Customer>();
        }

        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}

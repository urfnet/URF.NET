#region

using System.Collections.Generic;
using Northwind.Entities.Models;
using Repository.Pattern.Query;

#endregion

namespace Northwind.Repository.Customers
{
    public class CustomersByCountryQuery : IQuery<IEnumerable<Customer>>
    {
        public string Country { get; set; }
    }
}
#region

using System.Linq;
using Northwind.Data.Models;
using Repository;

#endregion

namespace Northwind.Repository
{
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(
            this IRepository<Customer> customerRepository,
            int customerId, int year)
        {
            return customerRepository
                .Find(customerId)
                .Orders.SelectMany(o => o.OrderDetails)
                .Select(o => o.Quantity*o.UnitPrice).Sum();
        }
    }
}
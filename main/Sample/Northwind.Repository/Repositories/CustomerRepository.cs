#region

using System.Linq;
using Northwind.Entities.Models;
using Repository.Pattern.Repositories;

#endregion

namespace Northwind.Repository
{
    // Exmaple: How to add custom methods to a repository.
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(
            this IRepositoryAsync<Customer> customerRepository,
            int customerId, int year)
        {
            return customerRepository
                .Find(customerId)
                .Orders.SelectMany(o => o.OrderDetails)
                .Select(o => o.Quantity*o.UnitPrice).Sum();
        }
    }
}
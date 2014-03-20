#region

using System.Collections.Generic;
using System.Linq;
using Northwind.Entities.Models;
using Repository.Pattern.Repositories;

#endregion

namespace Northwind.Repository.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(
            this IRepository<Customer> repository,
            string customerId, int year)
        {
            return repository
                .Find(customerId)
                .Orders.SelectMany(o => o.OrderDetails)
                .Select(o => o.Quantity*o.UnitPrice)
                .Sum();
        }

        public static IEnumerable<Customer> CustomersByCompany(
            this IRepositoryAsync<Customer> repository,
            string companyName)
        {
            return repository
                .Queryable()
                .Where(x => x.CompanyName.Contains(companyName))
                .AsEnumerable();
        }
    }
}
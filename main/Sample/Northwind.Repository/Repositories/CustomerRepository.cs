using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Northwind.Entities.Models;
using Northwind.Repository.Models;
using Repository.Pattern.Repositories;

namespace Northwind.Repository.Repositories
{
    // Exmaple: How to add custom methods to a repository.
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(this IRepository<Customer> repository, string customerId, int year)
        {
            return repository
                .Queryable()
                .Where(c => c.CustomerID == customerId)
                .SelectMany(c => c.Orders.Where(o => o.OrderDate != null && o.OrderDate.Value.Year == year))
                .SelectMany(c => c.OrderDetails)
                .Select(c => c.Quantity*c.UnitPrice)
                .Sum();
        }

        public static IEnumerable<Customer> CustomersByCompany(this IRepositoryAsync<Customer> repository, string companyName)
        {
            return repository
                .Queryable()
                .Where(x => x.CompanyName.Contains(companyName))
                .AsEnumerable();
        }

        public static IEnumerable<CustomerOrder> GetCustomerOrder(this IRepository<Customer> repository, string country)
        {
            var customers = repository.GetRepository<Customer>().Queryable();
            var orders = repository.GetRepository<Order>().Queryable();

            var query = from c in customers
                join o in orders on new {a = c.CustomerID, b = c.Country}
                    equals new {a = o.CustomerID, b = country}
                select new CustomerOrder
                {
                    CustomerId = c.CustomerID,
                    ContactName = c.ContactName,
                    OrderId = o.OrderID,
                    OrderDate = o.OrderDate
                };

            return query.AsEnumerable();
        }
    }
}
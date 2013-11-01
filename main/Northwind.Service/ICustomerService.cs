using System;
using System.Collections.Generic;
using Northwind.Entity.Models;

namespace Northwind.Service
{
    public interface ICustomerService : IDisposable
    {
        Customer Create(Customer customer);
        void Delete(string id);
        Customer GetCustomer(string customerId);
        IEnumerable<Customer> GetPagedList(int pageNumber, int pageSize, out int totalRecords);
        void Update(Customer customer);
        Customer Add(Customer customer);
    }
}
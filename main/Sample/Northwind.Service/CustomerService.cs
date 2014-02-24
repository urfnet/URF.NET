#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Northwind.Entitiy.Models;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        private bool _disposed;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Customer GetCustomer(string customerId)
        {
            return _unitOfWork.Repository<Customer>().Find(customerId);
        }

        public Customer Create(Customer customer)
        {
            customer.ObjectState = ObjectState.Added;
            _unitOfWork.Repository<Customer>().Insert(customer);
            return customer;
        }

        public void Delete(string id)
        {
            _unitOfWork.Repository<Customer>().Delete(id);
        }

        public void Update(Customer customer)
        {
            customer.ObjectState = ObjectState.Modified;
            _unitOfWork.Repository<Customer>().Update(customer);
        }

        public IEnumerable<Customer> GetPagedList(int pageNumber, int pageSize, out int totalRecords)
        {
            var customers = _unitOfWork.Repository<Customer>()
                .Query(q => !string.IsNullOrEmpty(q.ContactName))
                .OrderBy(q => q
                    .OrderBy(c => c.ContactName)
                    .ThenBy(c => c.CompanyName))
                .SelectPage(pageNumber, pageSize, out totalRecords);

            return customers;
        }

        public Customer Add(Customer customer)
        {
            _unitOfWork.Repository<Customer>().Insert(customer);
            return customer;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes the DbContext.
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _unitOfWork.Dispose();
            _disposed = true;
        }

    }
}
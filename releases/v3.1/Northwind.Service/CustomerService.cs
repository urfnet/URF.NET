#region

using System.Collections.Generic;
using System.Linq;
using Northwind.Data.Models;
using Repository;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWorks;

#endregion

namespace Northwind.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

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
                .Query()
                .OrderBy(q => q
                    .OrderBy(c => c.ContactName)
                    .ThenBy(c => c.CompanyName))
                .Filter(q => !string.IsNullOrEmpty(q.ContactName))
                .GetPage(pageNumber, pageSize, out totalRecords);

            return customers;
        }

        public Customer Add(Customer customer)
        {
            _unitOfWork.Repository<Customer>().Insert(customer);
            return customer;
        }

        public void Dispose()
        {
        }
    }
}
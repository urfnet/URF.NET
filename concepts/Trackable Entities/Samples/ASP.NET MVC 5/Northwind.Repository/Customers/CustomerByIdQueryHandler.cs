#region

using Northwind.Entities.Models;
using Repository.Pattern.Query;
using Repository.Pattern.Repository;

#endregion

namespace Northwind.Repository.Customers
{
    public class CustomerByIdQueryHandler : IQueryHandler<CustomerByIdQuery, Customer>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerByIdQueryHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer Handle(CustomerByIdQuery query)
        {
            return _customerRepository.Find(query);
        }
    }
}
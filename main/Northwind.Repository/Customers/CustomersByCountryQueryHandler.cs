using System.Collections.Generic;
using System.Linq;
using Northwind.Entities.Models;
using Repository.Pattern.Query;
using Repository.Pattern.Repository;

namespace Northwind.Repository.Customers
{
    public class CustomersByCountryQueryHandler : IQueryHandler<CustomersByCountryQuery, IEnumerable<Customer>>
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomersByCountryQueryHandler(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> Handle(CustomersByCountryQuery query)
        {
            return _customerRepository.Queryable().Where(x => x.Country == query.Country);
        }
    }
}
#region

using Northwind.Entities.Models;
using Repository.Pattern.Repositories;
using Northwind.Repository;
using Service.Pattern;

#endregion

namespace Northwind.Service
{
    public interface ICustomerService : IService<Customer>
    {
        // Add any custom business logic (methods) here
        // All methods in Service<TEntity> are ovverridable for any custom implementations
        decimal CustomerOrderTotalByYear(int customerId, int year);
    }

    // Add any custom business logic (methods) here
    // All methods in Service<TEntity> are ovverridable for any custom implementations
    // Can ovveride any of the Repository methods to add business logic in them
    // e.g.
    //public override void Delete(Customer entity)
    //{
    //    // Add business logic before or after deleting entity.
    //    base.Delete(entity);
    //}
    public class CustomerService : Service<Customer>, ICustomerService
    {
        private readonly IRepositoryAsync<Customer> _repository;

        public CustomerService(IRepositoryAsync<Customer> repository) : base(repository)
        {
            _repository = repository;
        }

        public decimal CustomerOrderTotalByYear(int customerId, int year)
        {
            return _repository.GetCustomerOrderTotalByYear(customerId, year);
        }

    }
}
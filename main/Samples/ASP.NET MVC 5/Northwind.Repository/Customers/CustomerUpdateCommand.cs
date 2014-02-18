#region

using Northwind.Entities.Models;
using Repository.Pattern.Command;
using Repository.Pattern.Repository;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Repository.Customers
{
    public class CustomerUpdateCommandHandler : ICommandHandler<CustomerUpdateCommand>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerUpdateCommandHandler(IUnitOfWork unitOfWork, IRepository<Customer> customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        public void Handle(CustomerUpdateCommand command)
        {
            _customerRepository.Update(command);
            _unitOfWork.SaveChanges();
        }
    }

    public class CustomerUpdateCommand : Customer
    {
        // TODO: This should really be a true command (e.g. DTO), for simplicity will just inherit the POCO
    }
}
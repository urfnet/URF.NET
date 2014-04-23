#region

using Northwind.Entities.Models;
using Repository.Pattern.Command;
using Repository.Pattern.Repository;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Repository.Customers
{
    public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Customer> _customeRepository;

        public CreateCustomerCommandHandler(IUnitOfWork unitOfWork, IRepository<Customer> customeRepository)
        {
            _unitOfWork = unitOfWork;
            _customeRepository = customeRepository;
        }

        public void Handle(CreateCustomerCommand command)
        {
            _customeRepository.Add(command.Customer);
            _unitOfWork.SaveChanges();
        }
    }
}
#region

using System.Linq;
using System.Web.Http.OData;
using Northwind.Data.Models;
using Repository;

#endregion

namespace Northwind.Web.Api
{
    public class CustomerController : EntitySetController<Customer, string>
    {
        private readonly IUnitOfWork _unitOfWork;


        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<Customer> Get()
        {
            return _unitOfWork.Repository<Customer>().Query().Get();
        }

        protected override Customer GetEntityByKey(string key)
        {
            return _unitOfWork.Repository<Customer>().Find(key);
        }

        protected override Customer UpdateEntity(string key, Customer update)
        {
            _unitOfWork.Repository<Customer>().Update(update);
            _unitOfWork.Save();
            return update;
        }

        public override void Delete(string key)
        {
            _unitOfWork.Repository<Customer>().Delete(key);
            _unitOfWork.Save();
        }
    }
}
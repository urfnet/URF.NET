#region

using System.Linq;
using System.Web.Http.OData;
using Northwind.Entity.Models;
using Repository;

#endregion

namespace Northwind.Web.Areas.Spa.Api
{
    public class ProductController : EntitySetController<Product, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override IQueryable<Product> Get()
        {
            return _unitOfWork.Repository<Product>().Query().Get();
        }

        protected override Product GetEntityByKey(int key)
        {
            return _unitOfWork.Repository<Product>().FindById(key);
        }

        protected override Product UpdateEntity(int key, Product update)
        {
            update.State = ObjectState.Modified;
            _unitOfWork.Repository<Product>().Update(update);
            _unitOfWork.Save();

            return update;
        }

        public override void Delete([FromODataUri] int key)
        {
            _unitOfWork.Repository<Product>().Delete(key);
            _unitOfWork.Save();
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
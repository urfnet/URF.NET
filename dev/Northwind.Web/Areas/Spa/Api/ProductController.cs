using System.Linq;
using System.Web.Http.OData;
using Northwind.Entity.Models;
using Repository;

namespace Northwind.Web.Areas.Spa.Api
{
    [ODataNullValue]
    public class ProductController : EntitySetController<Product, int>
    {
        #region Private Fields
        private readonly IUnitOfWork _db;
        #endregion Private Fields

        #region Constractor / Dispose
        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        #endregion Constractor / Dispose

        public override IQueryable<Product> Get()
        {
            return _db.Repository<Product>().Query().Get();
        }

        protected override Product GetEntityByKey(int key)
        {
            return _db.Repository<Product>().Find(key);
        }

        protected override Product UpdateEntity(int key, Product update)
        {
            update.State = ObjectState.Modified;
            _db.Repository<Product>().Update(update);
            _db.Save();

            return update;
        }

        public override void Delete([FromODataUri] int key)
        {
            _db.Repository<Product>().Delete(key);
            _db.Save();
        }
    }
}
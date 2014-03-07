using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Northwind.Service;
using Repository.Pattern.Infrastructure;
using Northwind.Entities.Models;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Web.Api
{
    public class ProductController : ODataController
    {
        private readonly IProductService _productService;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public ProductController(
            IUnitOfWorkAsync unitOfWork,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _productService = productService;
        }

        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All)]
        public IQueryable<Product> GetProduct()
        {
            return _productService.ODataQueryable();
        }

        // GET odata/Product(5)
        [Queryable]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(
                _productService
                    .Query(product => product.ProductID == key)
                    .Select()
                    .AsQueryable());
        }

        // PUT odata/Product(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != product.ProductID)
            {
                return BadRequest();
            }

			product.ObjectState = ObjectState.Modified;
			_productService.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST odata/Product
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			product.ObjectState = ObjectState.Added;
            _productService.Insert(product);
            await _unitOfWork.SaveChangesAsync();


            return Created(product);
        }

        // PATCH odata/Product(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

			product.ObjectState = ObjectState.Modified;
			_productService.Update(product);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE odata/Product(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var product = await _productService.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

			product.ObjectState = ObjectState.Deleted;
			_productService.Delete(product);
            await _unitOfWork.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Product(5)/Category
        [Queryable]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(
				_productService
				.Query(m => m.ProductID == key)
				.Select(m => m.Category)
				.AsQueryable());
        }

        // GET odata/Product(5)/OrderDetails
        [Queryable]
        public IQueryable<OrderDetail> GetOrderDetails([FromODataUri] int key)
        {
			var product = _productService
				.Query(m => m.ProductID == key)
				.Include(m => m.OrderDetails)
				.Select()
				.Single();

            return product.OrderDetails.AsQueryable();
        }

        // GET odata/Product(5)/Supplier
        [Queryable]
        public SingleResult<Supplier> GetSupplier([FromODataUri] int key)
        {
            return SingleResult.Create(
				_productService
				.Query(m => m.ProductID == key)
				.Select(m => m.Supplier)
				.AsQueryable());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
			return _productService.Query(e => e.ProductID == key).Select().Any();
        }
    }
}

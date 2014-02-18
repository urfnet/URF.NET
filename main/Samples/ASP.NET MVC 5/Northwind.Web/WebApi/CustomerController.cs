#region

using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Northwind.Entities.Models;
using Repository.Pattern.Repository;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;
using TrackableEntities.Common;

#endregion

namespace Northwind.Web.WebApi
{
    /*
    To add a route for this controller, merge these statements into the Register method of the WebApiConfig class. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using Northwind.Entities.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Customer>("Customer");
    builder.EntitySet<CustomerDemographic>("CustomerDemographic"); 
    builder.EntitySet<Order>("Order"); 
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */

    public class CustomerController : ODataController
    {
        private readonly IRepositoryAsync<Customer> _customerRepositoryAsync;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public CustomerController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IRepositoryAsync<Customer> customerRepositoryAsync)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerRepositoryAsync = customerRepositoryAsync;
        }

        // GET odata/Customer
        [Queryable]
        public IQueryable<Customer> GetCustomer()
        {
            return _customerRepositoryAsync.Queryable();
        }

        // GET odata/Customer(5)
        [Queryable]
        public SingleResult<Customer> GetCustomer(string key)
        {
            return SingleResult.Create(_customerRepositoryAsync.Queryable().Where(customer => customer.CustomerID == key));
        }

        // PUT odata/Customer(5)
        public async Task<IHttpActionResult> Put(string key, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != customer.CustomerID)
            {
                return BadRequest();
            }

            customer.TrackingState = TrackingState.Modified;
            _unitOfWorkAsync.ApplyChanges(customer);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
                customer.AcceptChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(customer);
        }

        // POST odata/Customer
        public async Task<IHttpActionResult> Post(Customer customer)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _customerRepositoryAsync.Add(customer);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerID))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(customer);
        }

        // PATCH odata/Customer(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch(string key, Delta<Customer> patch)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            var customer = await _customerRepositoryAsync.FindAsync(key);

            if (customer == null)
            {
                return NotFound();
            }

            patch.Patch(customer);

            try
            {
                _unitOfWorkAsync.ApplyChanges(customer);
                await _unitOfWorkAsync.SaveChangesAsync();
                customer.AcceptChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(customer);
        }

        // DELETE odata/Customer(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            var customer = await _customerRepositoryAsync.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepositoryAsync.Remove(customer);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Customer(5)/CustomerDemographics
        [Queryable]
        public IQueryable<CustomerDemographic> GetCustomerDemographics(string key)
        {
            return _customerRepositoryAsync.Queryable().Where(m => m.CustomerID == key).SelectMany(m => m.CustomerDemographics);
        }

        // GET odata/Customer(5)/Orders
        [Queryable]
        public IQueryable<Order> GetOrders(string key)
        {
            return _customerRepositoryAsync.Queryable().Where(m => m.CustomerID == key).SelectMany(m => m.Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWorkAsync.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string key)
        {
            return _customerRepositoryAsync.Queryable().Count(e => e.CustomerID == key) > 0;
        }
    }
}
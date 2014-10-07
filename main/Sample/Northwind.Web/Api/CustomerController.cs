using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using Northwind.Entities.Models;
using Northwind.Service;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Web.Api
{
    public class CustomerController : ODataController
    {
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public CustomerController(
            IUnitOfWorkAsync unitOfWorkAsync,
            ICustomerService customerService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerService = customerService;
        }

        // GET: odata/Customers
        [HttpGet]
        [Queryable]
        public IQueryable<Customer> GetCustomer()
        {
            return _customerService.Queryable();
        }

        // GET: odata/Customers(5)
        [Queryable]
        public SingleResult<Customer> GetCustomer([FromODataUri] string key)
        {
            return SingleResult.Create(_customerService.Queryable().Where(t => t.CustomerID == key));
        }

        // PUT: odata/Customers(5)
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

            customer.ObjectState = ObjectState.Modified;
            _customerService.Update(customer);

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
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

        // POST: odata/Customers
        public async Task<IHttpActionResult> Post(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.ObjectState = ObjectState.Added;
            _customerService.Insert(customer);

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

        //// PATCH: odata/Customers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Customer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = await _customerService.FindAsync(key);

            if (customer == null)
            {
                return NotFound();
            }

            patch.Patch(customer);
            customer.ObjectState = ObjectState.Modified;

            try
            {
                await _unitOfWorkAsync.SaveChangesAsync();
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

        // DELETE: odata/Customers(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            Customer customer = await _customerService.FindAsync(key);

            if (customer == null)
            {
                return NotFound();
            }

            customer.ObjectState = ObjectState.Deleted;

            _customerService.Delete(customer);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Customers(5)/CustomerDemographics
        [Queryable]
        public IQueryable<CustomerDemographic> GetCustomerDemographics([FromODataUri] string key)
        {
            return
                _customerService.Queryable()
                    .Where(m => m.CustomerID == key)
                    .SelectMany(m => m.CustomerDemographics);
        }

        // GET: odata/Customers(5)/Orders
        [Queryable]
        public IQueryable<Order> GetOrders([FromODataUri] string key)
        {
            return _customerService.Queryable().Where(m => m.CustomerID == key).SelectMany(m => m.Orders);
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
            return _customerService.Query(e => e.CustomerID == key).Select().Any();
        }
    }
}
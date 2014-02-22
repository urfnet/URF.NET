using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Northwind.Entitiy.Models;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Web.Api
{
    public class Customer2Controller : ODataController
    {
        private readonly IRepositoryAsync<Customer> _customerRepository;
        private readonly IUnitOfWorkAsync _unitOfWork;

        public Customer2Controller(
            IUnitOfWorkAsync unitOfWork,
            IRepositoryAsync<Customer> customerRepository)
        {
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
        }

        // GET odata/Customer
        public PageResult<Customer> GetCustomer(ODataQueryOptions<Customer> oDataQueryOptions)
        {
            var queryable = _customerRepository.ODataQueryable(oDataQueryOptions);

            return new PageResult<Customer>(
                queryable as IEnumerable<Customer>,
                Request.GetNextPageLink(),
                Request.GetInlineCount());
        }

        // GET odata/Customer(5)
        [Queryable]
        public SingleResult<Customer> GetCustomer(string key)
        {

            return SingleResult.Create(
                _customerRepository
                    .Query(customer => customer.CustomerID == key)
                    .Select()
                    .AsQueryable());
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

            customer.ObjectState = ObjectState.Modified;
            _customerRepository.Update(customer);

            try
            {
                await _unitOfWork.SaveChangesAsync();
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customerRepository.Insert(customer);

            try
            {
                await _unitOfWork.SaveChangesAsync();
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
        [System.Web.Mvc.AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch(string key, Delta<Customer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _customerRepository.FindAsync(key);

            if (customer == null)
            {
                return NotFound();
            }

            patch.Patch(customer);

            _customerRepository.Update(customer);

            try
            {
                customer.ObjectState = ObjectState.Modified;
                await _unitOfWork.SaveChangesAsync();
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
            var customer = await _customerRepository.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Customer(5)/CustomerDemographics
        [Queryable]
        public IQueryable<CustomerDemographic> GetCustomerDemographics(string key)
        {
            //return _customerRepositoryAsync.Queryable().Where(m => m.CustomerID == key).SelectMany(m => m.CustomerDemographics);
            throw new NotImplementedException();
        }

        // GET odata/Customer(5)/Orders
        [Queryable]
        public IQueryable<Order> GetOrders(string key)
        {
            throw new NotImplementedException();
            //return _customerRepositoryAsync.Queryable().Where(m => m.CustomerID == key).SelectMany(m => m.Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string key)
        {
            return _customerRepository.Query(e => e.CustomerID == key).Select().Any();
        }
    }
}
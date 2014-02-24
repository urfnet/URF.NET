#region

using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Northwind.Entitiy.Models;
using Northwind.Repository;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Web.Api
{
    public class CustomerController : ODataController
    {
        private readonly IRepositoryAsync<Customer> _customerRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public CustomerController(
            IUnitOfWorkAsync unitOfWorkAsync,
            IRepositoryAsync<Customer> customerRepository)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _customerRepository = customerRepository;
        }

        // GET odata/Customer
        public PageResult<Customer> GetCustomer(ODataQueryOptions<Customer> options)
        {
            var queryable = _customerRepository.ODataQueryable(options);

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

        // DELETE odata/Customer(5)
        public async Task<IHttpActionResult> Delete(string key)
        {
            var customer = await _customerRepository.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            _customerRepository.Delete(customer);
            await _unitOfWorkAsync.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Customer(5)/CustomerDemographics
        [Queryable]
        public IQueryable<CustomerDemographic> GetCustomerDemographics(string key)
        {
            return _customerRepository.Query(m => m.CustomerID == key)
                .Select()
                .AsQueryable()
                .SelectMany(m => m.CustomerDemographics);
        }

        // GET odata/Customer(5)/Orders
        [Queryable]
        public IQueryable<Order> GetOrders(string key)
        {
            return _customerRepository
                .Query(m => m.CustomerID == key)
                .Select()
                .AsQueryable()
                .SelectMany(m => m.Orders);
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
            return _customerRepository.Query(e => e.CustomerID == key).Select().Any();
        }
    }
}
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
using System.Web.Http.OData.Routing;
using Northwind.Entities.Models;

namespace Northwind.Web.Controllers
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
        private NorthwindContext db = new NorthwindContext();

        // GET odata/Customer
        [Queryable]
        public IQueryable<Customer> GetCustomer()
        {
            return db.Customers;
        }

        // GET odata/Customer(5)
        [Queryable]
        public SingleResult<Customer> GetCustomer([FromODataUri] string key)
        {
            return SingleResult.Create(db.Customers.Where(customer => customer.CustomerID == key));
        }

        // PUT odata/Customer(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != customer.CustomerID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
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

            db.Customers.Add(customer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(customer);
        }

        // PATCH odata/Customer(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Customer> patch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = await db.Customers.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            patch.Patch(customer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(customer);
        }

        // DELETE odata/Customer(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Customer customer = await db.Customers.FindAsync(key);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET odata/Customer(5)/CustomerDemographics
        [Queryable]
        public IQueryable<CustomerDemographic> GetCustomerDemographics([FromODataUri] string key)
        {
            return db.Customers.Where(m => m.CustomerID == key).SelectMany(m => m.CustomerDemographics);
        }

        // GET odata/Customer(5)/Orders
        [Queryable]
        public IQueryable<Order> GetOrders([FromODataUri] string key)
        {
            return db.Customers.Where(m => m.CustomerID == key).SelectMany(m => m.Orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(string key)
        {
            return db.Customers.Count(e => e.CustomerID == key) > 0;
        }
    }
}

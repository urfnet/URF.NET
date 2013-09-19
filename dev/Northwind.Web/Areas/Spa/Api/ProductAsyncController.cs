using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Microsoft.Data.OData;
using Northwind.Entity.Models;
using Northwind.Web.Areas.Spa.Extensions;
using Repository;

namespace Northwind.Web.Areas.Spa.Api
{
    [ODataNullValue]
    public class ProductAsyncController : AsyncEntitySetController<Product, int>
    {
        #region Private Fields
        private readonly IUnitOfWork _db;
        #endregion Private Fields

        #region Constractor / Dispose
        public ProductAsyncController(IUnitOfWork db)
        {
            _db = db;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        #endregion Constractor / Dispose

        /// <summary>
        /// Get the entity key of the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The entity key value.</returns>
        protected override int GetKey(Product entity)
        {
            return entity.ProductID;
        }

        // GET <controller>
        /// <summary>
        /// Handle GET requests that attempt to retrieve entities from the entity set.
        /// </summary>
        /// <returns>The matching entities from the entity set.</returns>
        [Queryable]
        public override async Task<IEnumerable<Product>> Get()
        {
            return await _db.Repository<Product>().Query().GetAsync();
        }

        // GET <controller>(key)
        /// <summary>
        /// Handles GET requests that attempt to retrieve an individual entity by key from the entity set.
        /// </summary>
        /// <param name="key">The entity key of the entity to retrieve.</param>
        /// <returns>Task that contains the response message to send back to the client.</returns>
        [Queryable]
        // ReSharper disable once CSharpWarnings::CS1998
        public override async Task<HttpResponseMessage> Get([FromODataUri] int key)
        {
            //This DOES work with OData $expand
            //Use GetSingleResult with the Microsoft.AspNet.WebApi.OData RTM from ASP.NET WebStack Nightly Builds at http://www.myget.org/F/aspnetwebstacknightly/
            //var query = _db.Repository<Product>().Query().Filter(x => x.ProductID == key).GetSingleResult();

            //Does NOT work with OData $expand
            var query = _db.Repository<Product>().Query().Filter(x => x.ProductID == key).Get();

            // Create an HttpResponseMessage and add singleResult
            return Request.CreateResponse(HttpStatusCode.OK, query);
        }

        #region Task<Product> GetEntityByKeyAsync(int key)
        ///// <summary>
        ///// Retrieve an entity by key from the entity set.
        ///// </summary>
        ///// <param name="key">The entity key of the entity to retrieve.</param>
        ///// <returns>A Task that contains the retrieved entity when it completes, or null if an entity with the specified entity key cannot be found in the entity set.</returns>
        //[Queryable]
        //protected override async Task<Product> GetEntityByKeyAsync(int key)
        //{
        //    return await _db.Repository<Product>().FindAsync(key);
        //}
        #endregion Task<Product> GetEntityByKeyAsync(int key)

        // POST <controller> - Insert
        /// <summary>
        /// Create a new entity in the entity set.
        /// </summary>
        /// <param name="entity"> The entity to add to the entity set.</param>
        /// <returns>A Task that contains the created entity when it completes.</returns>
        protected override async Task<Product> CreateEntityAsync(Product entity)
        {
            if (entity == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            _db.Repository<Product>().Insert(entity);
            await _db.SaveAsync();
            return entity;
        }

        // PUT <controller>(key) - Update
        /// <summary>
        /// Update an existing entity in the entity set.
        /// </summary>
        /// <param name="key">The entity key of the entity to update.</param>
        /// <param name="update">The updated entity.</param>
        /// <returns>A Task that contains the updated entity when it completes.</returns>
        protected override async Task<Product> UpdateEntityAsync(int key, Product update)
        {
            if (update == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (key != update.ProductID)
            {
                throw new HttpResponseException(Request.CreateODataErrorResponse(HttpStatusCode.BadRequest, new ODataError { Message = "The supplied key and the Product being updated do not match." }));
            }

            //TODO: Do we need this
            //if (await _db.Repository<Product>().FindAsync(key) == null)
            //{
            //    throw Request.EntityNotFound();
            //}
            //update.Id = key; // ignore the key in the entity use the key in the URL.

            try
            {
                _db.Repository<Product>().Update(update);
                await _db.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: ???
                //Handling Concurrency with the Entity Framework - http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/handling-concurrency-with-the-entity-framework-in-an-asp-net-mvc-application
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return update;
        }

        // PATCH <controller>(key)
        /// <summary>
        /// Apply a partial update to an existing entity in the entity set.
        /// </summary>
        /// <param name="key">The entity key of the entity to update.</param>
        /// <param name="patch">The patch representing the partial update.</param>
        /// <returns>A Task that contains the updated entity when it completes.</returns>
        protected override async Task<Product> PatchEntityAsync(int key, Delta<Product> patch)
        {
            if (patch == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (key != patch.GetEntity().ProductID)
            {
                throw Request.EntityNotFound();
            }

            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }

            try
            {
                patch.Patch(entity);
                await _db.SaveAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //TODO: ???
                //Handling Concurrency with the Entity Framework - http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/handling-concurrency-with-the-entity-framework-in-an-asp-net-mvc-application
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return entity;
        }

        // DELETE <controller>(key)
        /// <summary>
        /// Handles DELETE requests for deleting existing entities from the entity set.
        /// </summary>
        /// <param name="key">The entity key of the entity to delete.</param>
        /// <returns>A Task that completes when the entity has been successfully deleted.</returns>
        public override async Task Delete([FromODataUri] int key)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }

            //Delete children if any

            _db.Repository<Product>().Delete(entity);
            await _db.SaveAsync();
        }

        #region Links
        // Create a relation from Product to Category or Supplier, by creating a $link entity.
        // POST <controller>(key)/$links/Category
        // POST <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle POST and PUT requests that attempt to create a link between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <param name="link">The URI of the entity to link.</param>
        /// <returns>A Task that completes when the link has been successfully created.</returns>
        [AcceptVerbs("POST", "PUT")]
        public override async Task CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }
            switch (navigationProperty)
            {
                case "Category":
                    var categoryKey = Request.GetKeyValue<int>(link);
                    var category = await _db.Repository<Category>().FindAsync(categoryKey);

                    if (category == null)
                    {
                        throw Request.EntityNotFound();
                    }
                    entity.Category = category;
                    break;

                case "Supplier":
                    var supplierKey = Request.GetKeyValue<int>(link);
                    var supplier = await _db.Repository<Supplier>().FindAsync(supplierKey);

                    if (supplier == null)
                    {
                        throw Request.EntityNotFound();
                    }
                    entity.Supplier = supplier;
                    break;

                default:
                    await base.CreateLink(key, navigationProperty, link);
                    break;
            }
            await _db.SaveAsync();
        }

        // Remove a relation, by deleting a $link entity
        // DELETE <controller>(key)/$links/Category
        // DELETE <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle DELETE requests that attempt to break a relationship between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="relatedKey">The key of the related entity.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <returns>Task.</returns>
        public override async Task DeleteLink([FromODataUri] int key, string relatedKey, string navigationProperty)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }

            switch (navigationProperty)
            {
                case "Category":
                    entity.Category = null;
                    break;

                case "Supplier":
                    entity.Supplier = null;
                    break;

                default:
                    await base.DeleteLink(key, relatedKey, navigationProperty);
                    break;
            }
            await _db.SaveAsync();
        }

        // Remove a relation, by deleting a $link entity
        // DELETE <controller>(key)/$links/Category
        // DELETE <controller>(key)/$links/Supplier
        /// <summary>
        /// Handle DELETE requests that attempt to break a relationship between two entities.
        /// </summary>
        /// <param name="key">The key of the entity with the navigation property.</param>
        /// <param name="navigationProperty">The name of the navigation property.</param>
        /// <param name="link">The URI of the entity to remove from the navigation property.</param>
        /// <returns>Task.</returns>
        public override async Task DeleteLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }

            switch (navigationProperty)
            {
                case "Category":
                    entity.Category = null;
                    break;

                case "Supplier":
                    entity.Supplier = null;
                    break;

                default:
                    await base.DeleteLink(key, navigationProperty, link);
                    break;
            }
            await _db.SaveAsync();
        }
        #endregion Links

        /// <summary>
        /// Handle all unmapped OData requests.
        /// </summary>
        /// <param name="odataPath">The OData path of the request.</param>
        /// <returns>A Task that contains the response message to send back to the client when it completes.</returns>
        // ReSharper disable once CSharpWarnings::CS1998
        public override async Task<HttpResponseMessage> HandleUnmappedRequest(ODataPath odataPath)
        {
            // Create an HttpResponseMessage and add an HTTP header field
            //TODO: add logic and proper return values
            return Request.CreateResponse(HttpStatusCode.NoContent, odataPath);
        }

        #region Navigation Properties
        /// <summary>
        /// Get Category Navigation Property
        /// </summary>
        /// <param name="key">The entity key of the entity to retrieve.</param>
        /// <returns>A Task that contains the retrieved entity when it completes, or null if an entity with the specified entity key cannot be found in the entity set.</returns>
        public async Task<Category> GetCategory(int key)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }
            return entity.Category;
        }

        /// <summary>
        /// Get Supplier Navigation Property
        /// </summary>
        /// <param name="key">The entity key of the entity to retrieve.</param>
        /// <returns>A Task that contains the retrieved entity when it completes, or null if an entity with the specified entity key cannot be found in the entity set.</returns>
        public async Task<Supplier> GetSupplier(int key)
        {
            var entity = await _db.Repository<Product>().FindAsync(key);

            if (entity == null)
            {
                throw Request.EntityNotFound();
            }
            return entity.Supplier;
        }
        #endregion Navigation Properties
    }
}
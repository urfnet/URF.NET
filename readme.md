# URF #
**_<sup>Unit-of-Work & Repository Framework | Official URF Team</sup>_**
### Docs: [goo.gl/6zh9zp](https://goo.gl/6zh9zp) | Subscribe URF Updates: [@lelong37](http://twitter.com/lelong37) | NuGet: [goo.gl/WEn7Jm](https://goo.gl/WEn7Jm) ###

This framework ([over 100K+ total downloads](https://genericunitofworkandrepositories.codeplex.com)) minimizes the surface area of your ORM technology from disseminating in your application. This framework was deliberately designed to be lightweight, small in footprint size, and non-intimidating to extend and maintain. **When we say lightweight we really mean lightweight, when using this framework with the Entity Framework provider there are only 10 classes.** This lightweight framework will allow you to elegantly, unobtrusively, and easily patternize your applications and systems with Repository, Unit of Work, and Domain Driven Design. To use Generic Repositories or not? The framework allows the freedom of both, generic repositories and the ability to add in your own domain specific repository methods, in short **Unit of Work with extensible and generic Repositories**.

Live demo: [longle.azurewebsites.net](http://longle.azurewebsites.net)

### Architecture Overview (Sample Northwind Application with URF Framework) ###
![Architecture Overview (Sample Northwind Application & Framework)](https://lelong37.files.wordpress.com/2015/01/2015-01-03_19-15-001.png)

#### URF sample and usage in ASP.NET Web API ####

```csharp
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

        customer.TrackingState = TrackingState.Modified;
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

        customer.TrackingState = TrackingState.Added;
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
        customer.TrackingState = TrackingState.Modified;

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

        customer.TrackingState = TrackingState.Deleted;

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
```

#### Implementing Domain Logic with URF Service Pattern ###
All methods that are exposed from `Repository<TEntity>` in `Service<TEntity>` are overridable to add any pre or post domain/business logic. Domain business logic should be in the Service layer and not in Controllers or Repositories for separation of concerns.

1. Create an Interface e.g. `ICustomerService`, which should always inherit `IService<TEnttiy>` e.g. `IService<Customer>`
2. Implement the concrete implementation for your Interface e.g. `CustomerService` which implements `ICustomerService`
3. If using DI & IoC, don't forget to wire up the binding of your Interface and Implementation e.g. `container.RegisterType<ICustomerService, CustomerService>()`, see next example for more details on wiring up DI & IoC.

```csharp
public interface ICustomerService : IService<Customer>
{
    decimal CustomerOrderTotalByYear(string customerId, int year);
    IEnumerable<Customer> CustomersByCompany(string companyName);
    IEnumerable<CustomerOrder> GetCustomerOrder(string country);
}


public class CustomerService : Service<Customer>, ICustomerService
{
    private readonly IRepositoryAsync<Customer> _repository;

    public CustomerService(IRepositoryAsync<Customer> repository) : base(repository)
    {
        _repository = repository;
    }

    public decimal CustomerOrderTotalByYear(string customerId, int year)
    {
        // add any domain logic here
        return _repository.GetCustomerOrderTotalByYear(customerId, year);
    }

    public IEnumerable<Customer> CustomersByCompany(string companyName)
    {
        // add any domain logic here
        return _repository.CustomersByCompany(companyName);
    }

    public IEnumerable<CustomerOrder> GetCustomerOrder(string country)
    {
        // add any domain logic here
        return _repository.GetCustomerOrder(country);
    }

    public override void Insert(Customer entity)
    {
        // e.g. add any business logic here before inserting
        base.Insert(entity);
    }

    public override void Delete(object id)
    {
        // e.g. add business logic here before deleting
        base.Delete(id);
    }
}

```

#### URF Sample DI & IoC Configuration with Framework of your choice, exampled here using Microsoft Unity DI & IoC ####

[UnityConfig.cs](https://github.com/lelong37/URF/blob/master/main/Sample/Northwind.Web/App_Start/UnityConfig.cs)

```csharp
public class UnityConfig
{
    private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
    {
        var container = new UnityContainer();
        RegisterTypes(container);
        return container;
    });

    public static IUnityContainer GetConfiguredContainer()
    {
        return container.Value;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        container
            // Register DbContext instead of IDataDataContext, which is now obsolete.
            //.RegisterType<IDataContextAsync, NorthwindContext>(new PerRequestLifetimeManager())
            .RegisterType<DbContext, NorthwindContext>(new PerRequestLifetimeManager())
            .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerRequestLifetimeManager())
            .RegisterType<IRepositoryAsync<Customer>, Repository<Customer>>()
            .RegisterType<IRepositoryAsync<Product>, Repository<Product>>()
            .RegisterType<IProductService, ProductService>()
            .RegisterType<ICustomerService, CustomerService>()
            .RegisterType<INorthwindStoredProcedures, NorthwindContext>(new PerRequestLifetimeManager())
            .RegisterType<IStoredProcedureService, StoredProcedureService>();
    }
}

```

### Roadmap ###

URF v5 will have fully Trackable Entites with Trackable Entites and Observable Entities both for server-side and client-side e.g Angular, React, Vue.js, etc.

Status: Currently updating Sample Northind application.

https://github.com/TrackableEntities/trackable-entities-js  
https://github.com/TrackableEntities/observable-entities-js

- URF v5 Alpha ETA: 9/2017 - https://github.com/lelong37/URF/releases/tag/v5.0-alpha
- URF v5 Beta ETA: 10/2017 - https://github.com/lelong37/URF/releases/tag/v5.0-alpha
- URF v5 RC1 ETA: 10/2017 - https://github.com/lelong37/URF/releases/tag/v5.0-alpha

URF v5 major feature will include (self) Trackable Entities across physical boundaries without DbConext/DataConext, coming soon...! [Tony Sneed](https://twitter.com/tonysneed) from the [Trackable Entities Team](https://github.com/TrackableEntities/trackable-entities) will be leading this effort and collaboration..! Please tweet us [@tonysneed](https://twitter.com/tonysneed), [@lelong37](https://twitter.com/lelong37) for any questions or comments. Special thanks [@reddy6ue](https://github.com/reddy6ue) for helping out with migrating our docs from CodePlex.

### URF Features & Benefits ###

* **Repository Pattern** - 100% extensible ready
* **Unit of Work Pattern** - 100% atomic & transaction ready
* **Service Pattern** - pattern for implementing business, domain specific logic with 100% separation of concerns e.g. ICustomerService, IOrderService
* Minimize footprint of your ORM and data access layer
* **DI & IoC** 100% ready
* REST, Web API & **OData** 100% ready
* 100% testable & mockable
* 100% support for Stored Procedures
* Repository Pattern supports IEnumerable and/or IQueryable
* [Trackable Entities](https://github.com/TrackableEntities) - When using URF, entities are **100% automatically self tracking**, states are automatically trackable (New, Updated, Deleted, Unchanged), allowing entity or complex object graph states to be trackable across physical boundaries and application layers. Entity state can be tracked in Angular all the way to Web API.
* Full (Northwind) [sample application](https://github.com/lelong37/URF/tree/master/main/Sample) (Angular, Web API, OData, Entity Framework, SQL)
* 100% [unit tests & integration tests](https://github.com/lelong37/URF/blob/master/main/Sample/Northwind.Test/IntegrationTests/CustomerRepositoryTests.cs) - Integration tests, will drop and re-create NorthwindTest database everytime integration tests are ran

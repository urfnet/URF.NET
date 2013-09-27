#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository
{
    public interface IDbContext
    {
        DbContextConfiguration Configuration { get; }
        
        //TODO: What's the purose of _instanceId
        // This is for testing for DI & IoC frameworks ensuring that we are getting a new instance, the test
        // case was to ensure that the lifecyle of the UoW and DbContext was bound to the the lifetime of an 
        // HttpRequest, so for ever user they should have the same instnance throughout their HttpRequest however
        // the next HttpRequets they should get a new instance.
        Guid InstanceId { get; }
        
        //TODO: Discuss - Remove comment afterward
        //IDbSet does not define FindAsync
        //Remarks: System.Data.Entity.IDbSet<TEntity> was originally intended to allow creation of test doubles (mocks or fakes) for System.Data.Entity.DbSet<TEntity>.
        //However, this approach has issues in that adding new members to an interface breaks existing code that already implements the interface without the new members.
        //Therefore, starting with EF6, no new members will be added to this interface and it is recommended that System.Data.Entity.DbSet<TEntity> be used as the base class for test doubles.
        
        // Let's discuss this, not sure if there is a recommendation from you or not.
        DbSet<T> Set<T>() where T : class;

        //TODO: Discuss - This would eliminate the need for IObjectState, ObjectState and ApplyStateChanges()
        // We are decoupling everyone that consumes the UoW and Repo framework from all things related or in EntityFramework including DbEntityEntry
        // In the event if a client wants to use NHibernate or any other ORM we could do so, however if we have a dependency with DbEntityEntry which is 
        // pretty much EF related, this would require us to refactor all places that change manipulate the entity state with DbEntityEntry vs. State. If State is used
        // the scope for the swap would be only in ApplyChanges
        DbEntityEntry Entry(object o);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void Dispose();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        //TODO: What's the purose of ApplyStateChanges() and ObjectState?
        //Why can't we use the built-in state management?
        //In Repository.Update(TEntity entity) -> Context.Entry(entity).State = EntityState.Modified;

        // This is to decouple the application from EF and simply the complexity of for when a developer deals with 
        // an entity graph. For example you may want to edit a Customer entity, edit a PhoneNumber entity,  
        // add a PhoneNumber entity, edit an Order entity, add and remove OrderDetail all in one entity graph starting
        // with the Customer as the root entity. The State property will allows the developer to have
        void ApplyStateChanges();
    }
}
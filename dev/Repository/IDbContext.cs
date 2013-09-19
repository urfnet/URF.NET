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
        Guid InstanceId { get; }
        
        //TODO: Discuss - Remove comment afterward
        //IDbSet does not define FindAsync
        //Remarks: System.Data.Entity.IDbSet<TEntity> was originally intended to allow creation of test doubles (mocks or fakes) for System.Data.Entity.DbSet<TEntity>.
        //However, this approach has issues in that adding new members to an interface breaks existing code that already implements the interface without the new members.
        //Therefore, starting with EF6, no new members will be added to this interface and it is recommended that System.Data.Entity.DbSet<TEntity> be used as the base class for test doubles.
        DbSet<T> Set<T>() where T : class;

        //TODO: Discuss - This would eliminate the need for IObjectState, ObjectState and ApplyStateChanges()
        DbEntityEntry Entry(object o);

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void Dispose();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        //TODO: What's the purose of ApplyStateChanges() and ObjectState?
        //Why can't we use the built-in state management?
        //In Repository.Update(TEntity entity) -> Context.Entry(entity).State = EntityState.Modified;
        void ApplyStateChanges();
    }
}
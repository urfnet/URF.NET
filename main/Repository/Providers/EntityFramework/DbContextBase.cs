#region

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Providers.EntityFramework
{
    public class DbContextBase : DbContext, IDbContext
    {
        private readonly Guid _instanceId;

        public DbContextBase(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            _instanceId = Guid.NewGuid();
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        public void ApplyStateChanges()
        {
            foreach (DbEntityEntry dbEntityEntry in ChangeTracker.Entries())
            {
                var entityState = dbEntityEntry.Entity as IObjectState;

                if (entityState == null)
                {
                    throw new InvalidCastException("All entites must implement the IObjectState interface, " +
                                                   "this interface must be implemented so each entites state can explicitely determined when updating graphs.");
                }
                dbEntityEntry.State = StateHelper.ConvertState(entityState.ObjectState);
            }
        }

        public new DbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            ApplyStateChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            ApplyStateChanges();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            ApplyStateChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            Configuration.LazyLoadingEnabled = false;
            base.OnModelCreating(builder);
        }
        public void SyncObjectState(object entity)
        {
            Entry(entity).State = StateHelper.ConvertState(((IObjectState)entity).ObjectState);
        }
    }
}
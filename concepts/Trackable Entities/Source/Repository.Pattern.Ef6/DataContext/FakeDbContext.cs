#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repository;
using TrackableEntities;

#endregion

namespace Repository.Pattern.Ef6.DataContext
{
    public abstract class FakeDbContext : IDataContext
    {
        private readonly Dictionary<Type, object> _fakeDbSets;

        protected FakeDbContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
        }

        public Guid InstanceId { get; private set; }

        public DbSet<T> Set<T>() where T : class
        {
            return (DbSet<T>) _fakeDbSets[typeof (T)];
        }

        public int SaveChanges()
        {
            return default(int);
        }

        public void ApplyChanges(ITrackable trackable)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return new Task<int>(() => default(int));
        }

        public Task<int> SaveChangesAsync()
        {
            return new Task<int>(() => default(int));
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : class, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof (TEntity), fakeDbSet);
        }
    }
}
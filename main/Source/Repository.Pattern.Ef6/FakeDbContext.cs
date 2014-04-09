using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;

namespace Repository.Pattern.Ef6
{
    public interface IFakeDbContext
    {
        Guid InstanceId { get; }
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void Dispose();

        void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, IObjectState, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new();

        void SyncObjectState(object entity);
    }

    public abstract class FakeDbContext : IDataContextAsync, IFakeDbContext
    {
        #region Private Fields
        private readonly Dictionary<Type, object> _fakeDbSets;
        #endregion Private Fields

        protected FakeDbContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
            InstanceId = Guid.NewGuid(); //IF 04/09/2014
        }

        public int SaveChanges() { return default(int); }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken) { return new Task<int>(() => default(int)); }

        public Task<int> SaveChangesAsync() { return new Task<int>(() => default(int)); }

        public void Dispose() { }

        public void SyncObjectState(object entity) { }
        public Guid InstanceId { get; private set; }

        public DbSet<T> Set<T>() where T : class { return (DbSet<T>)_fakeDbSets[typeof(T)]; }

        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, IObjectState, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }
    }
}
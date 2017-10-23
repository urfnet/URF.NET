using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Ef6;
using TrackableEntities;

namespace Northwind.Test.UnitTests.Fake
{
    public class FakeDbContext : DbContext
    {
        private readonly Dictionary<Type, object> _fakeDbSets;

        protected FakeDbContext()
        {
            _fakeDbSets = new Dictionary<Type, object>();
        }

        public override int SaveChanges() => default(int);

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, ITrackable
        {
            // no implentation needed, unit tests which uses FakeDbContext since there is no actual database for unit tests, 
            // there is no actual DbContext to sync with, please look at the Integration Tests for test that will run against an actual database.
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken) => new Task<int>(() => default(int));

        public override Task<int> SaveChangesAsync() => new Task<int>(() => default(int));

        public override DbSet<TEntity> Set<TEntity>() => (DbSet<TEntity>)_fakeDbSets[typeof(TEntity)];

        public void AddFakeDbSet<TEntity, TFakeDbSet>()
            where TEntity : Entity, new()
            where TFakeDbSet : FakeDbSet<TEntity>, IDbSet<TEntity>, new()
        {
            var fakeDbSet = Activator.CreateInstance<TFakeDbSet>();
            _fakeDbSets.Add(typeof(TEntity), fakeDbSet);
        }
    }
}
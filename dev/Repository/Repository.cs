using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //TODO: What's the purose of _instanceId
        private readonly Guid _instanceId;

        internal IDbContext Context;

        //TODO: Discuss - Remove comment afterward
        //IDbSet does not define FindAsync
        //Remarks: System.Data.Entity.IDbSet<TEntity> was originally intended to allow creation of test doubles (mocks or fakes) for System.Data.Entity.DbSet<TEntity>.
        //However, this approach has issues in that adding new members to an interface breaks existing code that already implements the interface without the new members.
        //Therefore, starting with EF6, no new members will be added to this interface and it is recommended that System.Data.Entity.DbSet<TEntity> be used as the base class for test doubles.
        internal DbSet<TEntity> DbSet;

        public Repository(IDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
            _instanceId = Guid.NewGuid();
        }

        public Guid InstanceId
        {
            get { return _instanceId; }
        }

        //TODO: Discuss. Renamed to Find. FindById implies that there is one ID, but the method takes params object[] keyValues to enable composite keys.
        public virtual TEntity Find(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await DbSet.FindAsync(keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return DbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void InsertGraph(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);

            //TODO: Discuss - This would eliminate the need for IObjectState, ObjectState and ApplyStateChanges()
            // Added DbEntityEntry Entry(object o) to IDbContext
            //Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            var entity = DbSet.Find(id);

            ((IObjectState) entity).State = ObjectState.Deleted;
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public virtual IRepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper = new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
            {
                includeProperties.ForEach(i => query = query.Include(i));
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1)*pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query;
        }

        //TODO: Altough this does not do any async operations the AsyncEntitySetController class requires this signature.
        //Discuss!!!
        internal async Task<IEnumerable<TEntity>> GetAsync(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    List<Expression<Func<TEntity, object>>> includeProperties = null,
                    int? page = null,
                    int? pageSize = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (includeProperties != null)
            {
                includeProperties.ForEach(i => query = query.Include(i));
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (page != null && pageSize != null)
            {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            return (IEnumerable<TEntity>)query;
        }

    }
}
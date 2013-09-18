#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly Guid _instanceId;
        internal IDbContext Context;
        internal IDbSet<TEntity> DbSet;

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

        public virtual TEntity FindById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Attach(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entity = DbSet.Find(id);
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
                includeProperties.ForEach(i => query = query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1)*pageSize.Value)
                    .Take(pageSize.Value);

            IQueryable<TEntity> results = query;

            return results;
        }
    }
}
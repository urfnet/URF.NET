using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.OData.Query;
using LinqKit;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Repository.Pattern.Ef6
{
    public class Repository<TEntity> : IRepositoryAsync<TEntity> where TEntity : Entity
    {
        #region Private Fields
        private readonly IDataContextAsync _context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IUnitOfWorkAsync _unitOfWork;
        #endregion Private Fields

        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            // Temporarily for FakeDbContext, Unit Test and Fakes
            var dbContext = context as DbContext;

            if (dbContext != null)
            {
                _dbSet = dbContext.Set<TEntity>();
            }
            else
            {
                var fakeContext = context as FakeDbContext;

                if (fakeContext != null)
                {
                    _dbSet = fakeContext.Set<TEntity>();
                }
            }
        }

        public virtual TEntity Find(params object[] keyValues) { return _dbSet.Find(keyValues); }

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters) { return _dbSet.SqlQuery(query, parameters).AsQueryable(); }

        public virtual void Insert(TEntity entity)
        {
            ((IObjectState)entity).ObjectState = ObjectState.Added;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Insert(entity);
            }
        }

        public virtual void InsertGraph(TEntity entity) { _dbSet.Add(entity); }

        public virtual void InsertGraphRange(IEnumerable<TEntity> entities) { _dbSet.AddRange(entities); }

        public virtual void Update(TEntity entity)
        {
            ((IObjectState)entity).ObjectState = ObjectState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            ((IObjectState)entity).ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public IQueryFluent<TEntity> Query() { return new QueryFluent<TEntity>(this); }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject) { return new QueryFluent<TEntity>(this, queryObject); }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query) { return new QueryFluent<TEntity>(this, query); }
        public IQueryable Queryable(ODataQueryOptions<TEntity> oDataQueryOptions) { return oDataQueryOptions.ApplyTo(_dbSet); }

        public IQueryable<TEntity> Queryable() { return _dbSet; }

        public IRepository<T> GetRepository<T>() where T : IObjectState { return _unitOfWork.Repository<T>(); }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues) { return await _dbSet.FindAsync(keyValues); }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues) { return await _dbSet.FindAsync(cancellationToken, keyValues); }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues) { return await DeleteAsync(CancellationToken.None, keyValues); }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
            return true;
        }

        internal IQueryable<TEntity> Select(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

#pragma warning disable 1998
        internal async Task<IEnumerable<TEntity>> SelectAsync(
#pragma warning restore 1998
            Expression<Func<TEntity, bool>> query = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null,
            int? page = null,
            int? pageSize = null) { return Select(query, orderBy, includes, page, pageSize).AsEnumerable(); }
    }
}
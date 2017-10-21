using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TrackableEntities;

namespace Repository.Pattern.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, ITrackable
    {
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        [Obsolete("UpsertGraph has been deprecated. Instead set TrackingState on enttites in a graph.")]
        void UpsertGraph(TEntity entity);
        [Obsolete("InsertOrUpdateGraph has been deprecated. Instead set TrackingState on enttites in a graph.")]
        void InsertOrUpdateGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        IQueryFluent<TEntity> Query();
        IQueryable<TEntity> Queryable();
        IRepository<T> GetRepository<T>() where T : class, ITrackable;
    }
}
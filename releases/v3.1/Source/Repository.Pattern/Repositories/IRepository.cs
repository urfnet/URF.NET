using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Pattern.Repositories
{
    public interface IRepository<TEntity> where TEntity : Infrastructure.EntityBase
    {
        Guid InstanceId { get; }
        TEntity Find(params object[] keyValues);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IRepositoryQuery<TEntity> Query(Expression<Func<TEntity, bool>> clause = null);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData.Query;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;

namespace Service.Pattern
{
    public interface IService<TEntity> where TEntity : IObjectState
    {
        TEntity Find(params object[] keyValues);
        //IF 04/09/2014
        SingleResult<TEntity> GetSingleResult(params object[] keyValues);
        IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void InsertGraph(TEntity entity);
        void InsertGraphRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        IQueryFluent<TEntity> Query();
        IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject);
        IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query);
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable ODataQueryable(ODataQueryOptions<TEntity> oDataQueryOptions);
        IQueryable<TEntity> ODataQueryable();
    }
}
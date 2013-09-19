using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Guid InstanceId { get; }

        //TODO: Discuss. Renamed to Find. FindById implies that there is one ID, but the method takes params object[] keyValues to enable composite keys.
        TEntity Find(params object[] keyValues);
        
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
        void InsertGraph(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        IRepositoryQuery<TEntity> Query();
    }
}
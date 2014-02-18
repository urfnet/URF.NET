#region

using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repository;

#endregion

namespace Repository.Pattern.Ef6.Repository
{
    public class RepositoryAsync<TEntity> : Repository<TEntity>, IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        public RepositoryAsync(IDataContextAsync dataContextAsync) : base(dataContextAsync)
        {
            _dbContext = (DbContext) dataContextAsync;
        }

        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return _dbContext.Set<TEntity>().FindAsync(cancellationToken, keyValues);
        }
    }
}
#region

using System.Data.Entity;
using System.Linq;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repository;

#endregion

namespace Repository.Pattern.Ef6.Repository
{
    public class Repository<TEntity> :
        IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbConext;

        public Repository(IDataContext dataContextAsync)
        {
            _dbConext = (DbContext) dataContextAsync;
        }

        public TEntity Find(params object[] keyValues)
        {
            return _dbConext.Set<TEntity>().Find(keyValues);
        }

        public void Add(TEntity entity)
        {
            _dbConext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbConext.Set<TEntity>().Attach(entity);
        }

        public void Remove(params object[] keyValues)
        {
            var entity = Find(keyValues);
            _dbConext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Queryable()
        {
            return _dbConext.Set<TEntity>();
        }
    }
}
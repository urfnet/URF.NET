using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Pattern.Repositories
{
    public interface IRepositoryQuery<TEntity> where TEntity : Infrastructure.EntityBase
    {
        RepositoryQuery<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        RepositoryQuery<TEntity> Include(Expression<Func<TEntity, object>> expression);
        IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount);
        IEnumerable<TEntity> Select();
        Task<IEnumerable<TEntity>> SingleAsync();
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);
    }
}
#region

using System;
using System.Linq.Expressions;
using LinqKit;

#endregion

namespace Repository.Pattern.Repositories
{
    public abstract class QueryObject<TEntity> : IQueryObject<TEntity>
    {
        private Expression<Func<TEntity, bool>> _query;
        public virtual Expression<Func<TEntity, bool>> Query()
        {
            return _query;
        }

        public Expression<Func<TEntity, bool>> And(Expression<Func<TEntity, bool>> query)
        {
            return _query == null ? query : _query.And(query.Expand());
        }

        public Expression<Func<TEntity, bool>> Or(Expression<Func<TEntity, bool>> query)
        {
            return _query == null ? query : _query.Or(query.Expand());
        }

        public Expression<Func<TEntity, bool>> And(QueryObject<TEntity> queryObject)
        {
            return And(queryObject.Query());
        }

        public Expression<Func<TEntity, bool>> Or(QueryObject<TEntity> queryObject)
        {
            return Or(queryObject.Query());
        }

        protected void Add(Expression<Func<TEntity, bool>> predicate)
        {
            _query = (_query == null) ? predicate : _query.And(predicate.Expand());
        }
    }
}
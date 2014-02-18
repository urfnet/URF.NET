#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Pattern.Repository
{
    /// <summary>
    ///     Generic repository interface with basic asynchronous operations.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Finds an entity with the given primary key values. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>A task that represents the asynchronous find operation. The task result contains the entity found, or null.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        ///     Finds an entity with the given primary key values. If no entity is found, then null is returned.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <param name="keyValues">The values of the primary key for the entity to be found.</param>
        /// <returns>A task that represents the asynchronous find operation. The task result contains the entity found, or null.</returns>
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
    }
}
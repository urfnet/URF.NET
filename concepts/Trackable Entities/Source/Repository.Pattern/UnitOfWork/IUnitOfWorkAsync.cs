#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Pattern.UnitOfWork
{
    /// <summary>
    ///     Unit of work for committing changes across one or more repositories asynchronously.
    /// </summary>
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        /// <summary>
        ///     Saves changes made to one or more repositories.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of objects saved.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        ///     Saves changes made to one or more repositories.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of objects saved.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
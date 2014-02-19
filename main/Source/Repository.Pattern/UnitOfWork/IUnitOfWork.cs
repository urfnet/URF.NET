#region

using System;

#endregion

namespace Repository.Pattern.UnitOfWork
{
    /// <summary>
    ///     Unit of work for committing changes across one or more repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Saves changes made to one or more repositories.
        /// </summary>
        /// <returns>The number of objects saved.</returns>
        int SaveChanges();
    }
}
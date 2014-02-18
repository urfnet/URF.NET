#region

using System;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;

#endregion

namespace RepositoryPattern.Ef6.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dataContextAsync;
        private bool _disposed;

        public UnitOfWork(IDataContext dataContextAsync)
        {
            _dataContextAsync = dataContextAsync;
        }

        /// <summary>
        ///     Disposes the DbContext.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Disposes the DbContext.
        /// </summary>
        /// <param name="disposing">
        ///     True to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing) _dataContextAsync.Dispose();
            _disposed = true;
        }

        /// <summary>
        ///     Saves changes made to one or more repositories.
        /// </summary>
        /// <returns>The number of objects saved.</returns>
        public virtual int SaveChanges()
        {
            if (_disposed)
                throw new ObjectDisposedException("UnitOfWork");
            return _dataContextAsync.SaveChanges();
        }

        public void ApplyChanges(ITrackable trackable)
        {
            _dataContextAsync.ApplyChanges(trackable);
        }
    }
}
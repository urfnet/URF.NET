#region

using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Repository.Pattern.Ef6
{
    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Private Fields

        private IDataContextAsync _dataContext;
        private bool _disposed;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContextAsync dataContext, IRepositoryProvider repositoryProvider)
        {
            RepositoryProvider = repositoryProvider;
            RepositoryProvider.DataContext = _dataContext = dataContext;
            RepositoryProvider.UnitOfWork = this;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only

                try
                {
                    if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                    {
                        _objectContext.Connection.Close();
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }

                if (_dataContext != null)
                {
                    _dataContext.Dispose();
                    _dataContext = null;
                }
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }

        #endregion Constuctor/Dispose

        protected IRepositoryProvider RepositoryProvider { get; set; }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState
        {
            return RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState
        {
            return RepositoryProvider.GetRepositoryForEntityType<TEntity>();
        }

        #region Unit of Work Transactions

        //IF 04/09/2014 Add IsolationLevel
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter) _dataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dataContext.SyncObjectsStatePostCommit();
        }

        #endregion

        // Uncomment, if rather have IRepositoryAsync<TEntity> IoC vs. Reflection Activation
        //public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : EntityBase
        //{
        //    return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
        //}
    }
}
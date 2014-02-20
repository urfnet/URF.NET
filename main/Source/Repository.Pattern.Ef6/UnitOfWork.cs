#region

using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Repository.Pattern.DataContext;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Repository.Pattern.Ef6
{
    public class UnitOfWork : IUnitOfWork, IUnitOfWorkAsync
    {
        #region Private Fields

        private readonly DataContext _context;
        private bool _disposed;
        private ObjectContext _objectContext;
        private Hashtable _repositories;
        private DbTransaction _transaction;

        #endregion Private Fields

        #region Constuctor/Dispose

        public UnitOfWork(IDataContextAsync context)
        {
            _context = (DataContext) context;
        }

        public void Dispose()
        {
            if (_objectContext.Connection.State == ConnectionState.Open)
                _objectContext.Connection.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        #endregion Constuctor/Dispose

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : Entity
        {
            return RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : Entity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof (TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>) _repositories[type];
            }

            var repositoryType = typeof (Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof (TEntity)), _context));

            return (IRepositoryAsync<TEntity>) _repositories[type];
        }

        // Uncomment, if rather have IRepositoryAsync<TEntity> IoC vs. Reflection Activation
        //public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : EntityBase
        //{
        //    return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
        //}

        #region Unit of Work Transactions

        public void BeginTransaction()
        {
            _objectContext = ((IObjectContextAdapter) _context).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
                _transaction = _objectContext.Connection.BeginTransaction();
            }
        }

        public int Commit()
        {
            var saveChanges = _context.SaveChanges();
            _transaction.Commit();
            return saveChanges;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _context.SyncObjectsStatePostCommit();
        }

        public Task<int> CommitAsync()
        {
            var saveChangesAsync = SaveChangesAsync();
            _transaction.Commit();
            return saveChangesAsync;
        }

        #endregion
    }
}
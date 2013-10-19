using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        #region Private Fields
        private readonly IDbContext _context;
        private Hashtable _repositories;
        private bool _disposed;
        private readonly Guid _instanceId;
        #endregion Private Fields

        #region Constuctor/Dispose
        public UnitOfWork(IDbContext context)
        {
            _context = context;
            _context.Configuration.LazyLoadingEnabled = false;
            _instanceId = Guid.NewGuid();
        }

        public void Dispose()
        {
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

        public Guid InstanceId { get { return _instanceId; } }

        public void Save()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            string type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type))
            {
                return (IRepository<TEntity>)_repositories[type];
            }

            var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context));

            return (IRepository<TEntity>)_repositories[type];
        }

        public IEnumerable<DbEntityValidationResult> GetValidationErrors()
        {
            return _context.GetValidationErrors();
        }
    }
}
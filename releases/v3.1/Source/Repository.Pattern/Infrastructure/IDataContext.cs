#region

using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Pattern.Infrastructure
{
    public interface IDataContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void SyncObjectState(object entity);
    }

    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}
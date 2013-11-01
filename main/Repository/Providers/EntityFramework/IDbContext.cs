#region

using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Providers.EntityFramework
{
    public interface IDbContext : IDisposable
    {
        Guid InstanceId { get; }
        IDbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void Dispose();
        void ApplyStateChanges();
        void SyncObjectState(object entity);
    }
}
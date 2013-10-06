#region

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository
{
    public interface IDbContext
    {
        DbContextConfiguration Configuration { get; }
        
        Guid InstanceId { get; }
        
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
        void Dispose();
        IEnumerable<DbEntityValidationResult> GetValidationErrors();

        void ApplyStateChanges();
    }
}
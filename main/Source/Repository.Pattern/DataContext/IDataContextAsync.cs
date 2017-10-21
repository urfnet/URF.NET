using System;
using System.Threading;
using System.Threading.Tasks;

namespace Repository.Pattern.DataContext
{
    [Obsolete("IDataContextAsync has been deprecated. Instead use UnitOfWork which uses DbContext.")]
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}
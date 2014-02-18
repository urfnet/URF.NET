#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Repository.Pattern.DataContext
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
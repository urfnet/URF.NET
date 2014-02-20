#region

using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;

#endregion

namespace Repository.Pattern.UnitOfWorks
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveAsync();
        Task<int> SaveAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : Infrastructure.EntityBase;
    }
}
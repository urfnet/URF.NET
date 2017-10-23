using System;
using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.Repositories;
using TrackableEntities;

namespace Repository.Pattern.UnitOfWork
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable;
    }
}
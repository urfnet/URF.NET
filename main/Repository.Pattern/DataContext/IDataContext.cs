#region

using System.Data.Entity;
using TrackableEntities;

#endregion

namespace Repository.Pattern.DataContext
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        void Dispose();
        int SaveChanges();
        void ApplyChanges(ITrackable trackable);
    }
}
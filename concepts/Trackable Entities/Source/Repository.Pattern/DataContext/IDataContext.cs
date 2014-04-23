#region

using System;
using System.Data.Entity;
using TrackableEntities;

#endregion

namespace Repository.Pattern.DataContext
{
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
        void ApplyChanges(ITrackable trackable);
    }
}
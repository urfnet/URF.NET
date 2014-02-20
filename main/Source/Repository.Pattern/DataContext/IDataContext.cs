#region

using System;
using System.Data.Entity;

#endregion

namespace Repository.Pattern.DataContext
{
    public interface IDataContext : IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        void SyncObjectState(object entity);
    }
}
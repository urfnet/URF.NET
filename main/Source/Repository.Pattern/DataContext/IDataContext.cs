#region

using System;

#endregion

namespace Repository.Pattern.DataContext
{
    // This is a test commit
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState(object entity);
    }
}
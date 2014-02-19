#region

using System;
using System.Data.Entity;

#endregion

namespace Repository.Pattern.DataContext
{
    public interface IDataContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
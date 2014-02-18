#region

using System;
using System.Data.Entity;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repository;
using Repository.Pattern.Ef6.Repository;
using TrackableEntities;
using TrackableEntities.EF6;

#endregion

namespace Repository.Pattern.Ef6.DataContext
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        void IDataContext.ApplyChanges(ITrackable trackable)
        {
            this.ApplyChanges(trackable);
        }
    }
}
#region



#endregion

namespace Repository
{
    public interface IUnitOfWork : IUnitOfWorkForService
    {
        void Save();
        void Dispose(bool disposing);
        void Dispose();
    }

    public interface IUnitOfWorkForService
    {
        IRepository<T> Repository<T>() where T : class, new();
    }
}
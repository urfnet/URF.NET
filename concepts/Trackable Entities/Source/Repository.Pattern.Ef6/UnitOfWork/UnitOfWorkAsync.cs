#region

using System.Threading;
using System.Threading.Tasks;
using Repository.Pattern.DataContext;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Repository.Pattern.Ef6.UnitOfWork
{
    public class UnitOfWorkAsync : UnitOfWork, IUnitOfWorkAsync
    {
        private readonly IDataContextAsync _dataContextAsync;

        public UnitOfWorkAsync(IDataContextAsync dataContextAsync)
            : base(dataContextAsync)
        {
            _dataContextAsync = dataContextAsync;
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContextAsync.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContextAsync.SaveChangesAsync(cancellationToken);
        }
    }
}
using Repository.Pattern.DataContext;

namespace Repository.Pattern.Ef6.DataContext
{
    public class DataContextAsync : DataContext, IDataContextAsync
    {
        public DataContextAsync(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}
#region

using Northwind.Data.Models;
using Northwind.Entities.Models;
using Repository.Providers.EntityFramework.Fakes;
using RepositoryPattern.Ef6.DataContext;

#endregion

namespace Northwind.Test.Fake
{
    public class NorthwindFakeContext : FakeDbContext
    {
        public NorthwindFakeContext()
        {
            //AddFakeDbSet<Category, CategoryDbSet>();
        }
    }
}

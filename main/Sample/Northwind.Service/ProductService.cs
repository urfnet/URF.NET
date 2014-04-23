#region

using Northwind.Entities.Models;
using Repository.Pattern.Repositories;
using Service.Pattern;

#endregion

namespace Northwind.Service
{
    public interface IProductService : IService<Product>
    {
    }

    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IRepositoryAsync<Product> repository) : base(repository)
        {
        }
    }
}
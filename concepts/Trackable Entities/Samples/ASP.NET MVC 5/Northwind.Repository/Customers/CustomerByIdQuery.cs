using Northwind.Entities.Models;
using Repository.Pattern.Query;

namespace Northwind.Repository.Customers
{
    public class CustomerByIdQuery : IQuery<Customer>
    { 
        public string Id { get; set; }
    }
}
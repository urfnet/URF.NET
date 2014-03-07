using System.Linq;
using Northwind.Entities.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;

namespace Northwind.Repository.Queries
{
    public class CustomerSalesQuery : QueryObject<Customer>
    {
        public CustomerSalesQuery WithPurchasesMoreThan(decimal amount)
        {
            Add(x => x.Orders
                .SelectMany(y => y.OrderDetails)
                .Sum(z => z.UnitPrice * z.Quantity) > amount);

            return this;
        }

        public CustomerSalesQuery WithQuantitiesMoreThan(decimal quantity)
        {
            Add(x => x.Orders
                .SelectMany(y => y.OrderDetails)
                .Sum(z => z.Quantity) > quantity);

            return this;
        }
    }
}
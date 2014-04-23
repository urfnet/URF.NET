#region

using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Northwind.Entity.Models;
using Repository;

#endregion

namespace Northwind.Repository
{
    public static class CustomerRepository
    {
        public static decimal GetCustomerOrderTotalByYear(
            this IRepository<Customer> customerRepository,
            int customerId, int year)
        {
            return customerRepository
                .Find(customerId)
                .Orders.SelectMany(o => o.OrderDetails)
                .Select(o => o.Quantity*o.UnitPrice).Sum();
        }

        /// <summary>
        ///     m
        ///     TODO:
        ///     This should really live in the Services project (Business Layer),
        ///     however, we'll leave it here for now as an example, and migrate
        ///     this in the next post.
        /// </summary>
        public static void AddCustomerWithAddressValidation(
            this IRepository<Customer> customerRepository, Customer customer)
        {
            // Psuedo fictitious example of accessing other Repositories
            var unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();

            var invoices = unitOfWork
                .Repository<Invoice>()
                .Query()
                .Filter(i => i.CustomerID == customer.CustomerID);

            // Psuedo code
            //USPSManager m = new USPSManager("YOUR_USER_ID", true);
            //Address a = new Address();
            //a.Address1 = customer.Address;
            //a.City = customer.City;

            //Address validatedAddress = m.ValidateAddress(a);

            //if (validatedAddress != null)
            //    customerRepository.InsertGraph(customer);
        }
    }
}
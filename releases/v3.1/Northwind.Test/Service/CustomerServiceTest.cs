#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Data.Models;
using Northwind.Service;
using Northwind.Test.Fake;
using Repository;
using Repository.Pattern;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWorks;
using Repository.Providers.EntityFramework;

#endregion

namespace Northwind.Test.Service
{
    [TestClass]
    public class CustomerServiceTest
    {
        [TestMethod]
        public void AddNewCustomer()
        {
            using (IDataContext context = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(context))
            using (ICustomerService customerService = new CustomerService(unitOfWork))
            {
                var newCustomer = new Customer
                {
                    Address = "2100 Ross Ave",
                    City = "Dallas",
                    CompanyName = "CBRE",
                    ContactTitle = "Software Engineer",
                    Country = "US",
                    CustomerID = "CBRE",
                    Fax = "2222222222",
                    Phone = "1111111111",
                    PostalCode = "75042",
                    Region = "Dallas"
                };

                customerService.Add(newCustomer);

                unitOfWork.Save();

                Customer savedCustomer = customerService.GetCustomer("CBRE");

                Assert.AreEqual(newCustomer.CustomerID, savedCustomer.CustomerID);
            }
        }
    }
}
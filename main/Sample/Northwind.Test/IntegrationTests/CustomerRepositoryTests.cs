#region

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Northwind.Repository.Models;
using Northwind.Repository.Repositories;
using Northwind.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Service.Pattern;

#endregion

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Utility.CreateSeededTestDatabase();
        }

        [TestMethod]
        public void CreateCustomerTest()
        {
            // Create new customer
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);

                var customer = new Customer
                {
                    CustomerID = "LLE37",
                    CompanyName = "CBRE",
                    ContactName = "Long Le",
                    ContactTitle = "App/Dev Architect",
                    Address = "11111 Sky Ranch",
                    City = "Dallas",
                    PostalCode = "75042",
                    Country = "USA",
                    Phone ="(222) 222-2222",
                    Fax = "(333) 333-3333",
                    ObjectState = ObjectState.Added,
                };

                customerRepository.Insert(customer);
                unitOfWork.SaveChanges();
            }

            //  Query for newly created customer by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                var customer = customerRepository.Find("LLE37");
                Assert.AreEqual(customer.CustomerID, "LLE37"); 
            }
        }

        [TestMethod]
        public void CreateAndUpdateAndDeleteCustomerGraphTest()
        {
            // Create new customer
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);

                var customerForInsertGraphTest = new Customer
                {
                    CustomerID = "LLE38",
                    CompanyName = "CBRE",
                    ContactName = "Long Le",
                    ContactTitle = "App/Dev Architect",
                    Address = "11111 Sky Ranch",
                    City = "Dallas",
                    PostalCode = "75042",
                    Country = "USA",
                    Phone = "(222) 222-2222",
                    Fax = "(333) 333-3333",
                    ObjectState = ObjectState.Added,
                    Orders = new[]
                    {
                        new Order()
                        {
                            CustomerID = "LLE38",
                            EmployeeID = 1,
                            OrderDate = DateTime.Now,
                            ObjectState = ObjectState.Added,
                        }, 
                        new Order()
                        {
                            CustomerID = "LLE39",
                            EmployeeID = 1,
                            OrderDate = DateTime.Now,
                            ObjectState = ObjectState.Added
                        }, 
                    }
                };

                customerRepository.InsertOrUpdateGraph(customerForInsertGraphTest);
                unitOfWork.SaveChanges();
            }

            Customer customerForUpdateDeleteGraphTest = null;

            //  Query for newly created customer by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                
                 customerForUpdateDeleteGraphTest = customerRepository
                    .Query(x => x.CustomerID == "LLE38")
                    .Include(x => x.Orders)
                    .Select()
                    .SingleOrDefault();

                // Testing that customer was created
                Assert.AreEqual(customerForUpdateDeleteGraphTest.CustomerID, "LLE38");

                // Testing that orders in customer graph were created
                Assert.IsTrue(customerForUpdateDeleteGraphTest.Orders.Count == 2);

                // Make changes to the object graph while in this context, will save these 
                // changes in another context, testing managing states between and/or while disconnected
                // from the orginal DataContext

                // Updating the customer in the graph
                customerForUpdateDeleteGraphTest.City = "Houston";
                customerForUpdateDeleteGraphTest.ObjectState = ObjectState.Modified;

                // Updating the order in the graph
                var firstOrder = customerForUpdateDeleteGraphTest.Orders.Take(1).Single();
                firstOrder.ShipCity = "Houston";
                firstOrder.ObjectState = ObjectState.Modified;

                // Deleting one of the orders from the graph
                var secondOrder = customerForUpdateDeleteGraphTest.Orders.Skip(1).Take(1).Single();
                secondOrder.ObjectState = ObjectState.Deleted;
            }

            //  Query for newly created customer by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);

                // Testing changes to graph while disconncted from it's orginal DataContext
                // Saving changes while graph was previous DataContext that was already disposed
                customerRepository.InsertOrUpdateGraph(customerForUpdateDeleteGraphTest);
                unitOfWork.SaveChanges();

                customerForUpdateDeleteGraphTest = customerRepository
                    .Query(x => x.CustomerID == "LLE38")
                    .Include(x => x.Orders)
                    .Select()
                    .SingleOrDefault();

                Assert.AreEqual(customerForUpdateDeleteGraphTest.CustomerID, "LLE38");

                // Testing for order(2) was deleted from the graph
                Assert.IsTrue(customerForUpdateDeleteGraphTest.Orders.Count == 1);

                // Testing that customer was updated in the graph
                Assert.IsTrue(customerForUpdateDeleteGraphTest.City == "Houston");

                // Testing that order was updated in the graph.
                Assert.IsTrue(customerForUpdateDeleteGraphTest.Orders.ToArray()[0].ShipCity == "Houston");
            }
        }

        [TestMethod]
        public void GetCustomerOrderTest()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                var customerOrders = customerRepository.GetCustomerOrder("USA");
                var enumerable = customerOrders as CustomerOrder[] ?? customerOrders.ToArray();
                TestContext.WriteLine("Customers found: {0}", enumerable.Count());
                Assert.IsTrue(enumerable.Any());
            }
        }

        [TestMethod]
        public void FindCustomerById_Test()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                IService<Customer> customerService = new CustomerService(customerRepository);
                var customer = customerService.Find("ALFKI");
                TestContext.WriteLine("Customers found: {0}", customer.ContactName);
                Assert.AreEqual(customer.ContactName, "Maria Anders");
            }
        }

        [TestMethod]
        public void CustomerOrderTotalByYear()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                ICustomerService customerService = new CustomerService(customerRepository);
                var customerOrderTotalByYear = customerService.CustomerOrderTotalByYear("ALFKI", 1998);
                Assert.AreEqual(customerOrderTotalByYear, (decimal)2302.2000);
            }
        }

        [TestMethod]
        public void GetCustomersAsync()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                ICustomerService customerService = new CustomerService(customerRepository);

                var asyncTask = customerService
                    .Query(x => x.Country == "USA")
                    .Include(x => x
                        .Orders
                        .Select(y => y.OrderDetails))
                    .OrderBy(x => x
                        .OrderBy(y => y.CompanyName)
                        .ThenBy(z => z.ContactName))
                    .SelectAsync();

                var customers = asyncTask.Result;

                Assert.IsTrue(customers.Count() > 1);
                Assert.IsFalse(customers.Count(x => x.Country == "USA") == 0);
            }
        }
    }
}
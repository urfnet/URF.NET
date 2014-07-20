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
using Repository.Pattern.Ef6.Factories;
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
        private static readonly string MasterConnectionString = ConfigurationManager.ConnectionStrings["MasterDbConnection"].ConnectionString;
        private readonly IRepositoryProvider _repositoryProvider = new RepositoryProvider(new RepositoryFactories());

        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void SettingUpNorthwindTestDatabase()
        {
            TestContext.WriteLine("Please ensure Northwind.Test/Sql/instnwnd.sql is copied to C:\\temp\\instnwnd.sql for test to run succesfully");
            TestContext.WriteLine("Please verify the the Northwind.Test/app.config connection strings are correct for your environment");

            TestContext.WriteLine("TestFixture executing, creating NorthwindTest Db for integration  tests");
            TestContext.WriteLine("Loading and parsing create NorthwindTest database Sql script");

            var file = new FileInfo("C:\\temp\\instnwnd.sql");
            var script = file.OpenText().ReadToEnd();
            RunSqlOnMaster(script);
            TestContext.WriteLine("NorthwindTest Db created for integration tests");
        }

        [TestCleanup]
        public void Cleanup()
        {
            // There is a state issue where NorthwindTest exists in the master database, however the actual NorthwindTest Db does not exists
            // We can just leave the TestInitialize method which will drop the Db before recreating it for until we figure this out.

            //  kill any live transactions
            //const string script1 = "ALTER DATABASE NorthwindTest SET READ_ONLY WITH ROLLBACK IMMEDIATE";
            //  drop the db and deletes the files on disk
            //const string script2 = "DROP DATABASE NorthwindTest;";
            //RunSqlOnMaster(script1);
            //RunSqlOnMaster(script2);
        }

        private static void RunSqlOnMaster(string script)
        {
            using (var connection = new SqlConnection(MasterConnectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }

        [TestMethod]
        public void CreateCustomerTest()
        {
            // Create new customer
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
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
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                var customer = customerRepository.Find("LLE37");
                Assert.AreEqual(customer.CustomerID, "LLE37"); 
            }
        }

        [TestMethod]
        public void CreateCustomerGraphTest()
        {
            // Create new customer
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);

                var customer = new Customer
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
                            ObjectState = ObjectState.Added
                        }, 
                    }
                };

                customerRepository.InsertGraph(customer);
                unitOfWork.SaveChanges();
            }

            //  Query for newly created customer by ID from a new context, to ensure it's not pulling from cache
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                
                var customer = customerRepository
                    .Query(x => x.CustomerID == "LLE38")
                    .Include(x => x.Orders)
                    .Select()
                    .SingleOrDefault();

                Assert.AreEqual(customer.CustomerID, "LLE38");
                Assert.IsTrue(customer.Orders.Any());
            }
        }

        [TestMethod]
        public void GetCustomerOrderTest()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
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
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
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
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                ICustomerService customerService = new CustomerService(customerRepository);
                var customerOrderTotalByYear = customerService.CustomerOrderTotalByYear("ALFKI", 1998);
                Assert.AreEqual(customerOrderTotalByYear, (decimal)2302.2000);
            }
        }
    }
}
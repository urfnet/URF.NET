using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Northwind.Entities.Models;
using Northwind.Repository.Models;
using Northwind.Service;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Northwind.Repository.Repositories;
using Service.Pattern;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        readonly IRepositoryProvider _repositoryProvider = new RepositoryProvider(new RepositoryFactories());
        const string sqlConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True";

        [TestInitialize]
        public void SettingUpNorthwindTestDatabase()
        {
            TestContext.WriteLine("Please ensure Northwind.Test/Sql/instnwnd.sql is copied to C:\\temp\\instnwnd.sql for test to run succesfully");
            TestContext.WriteLine("Please verify the the Northwind.Test/app.config connection string is correct for your environment");

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
            //  kill any live transactions
            var script1 = "ALTER DATABASE NorthwindTest SET READ_ONLY WITH ROLLBACK IMMEDIATE";
            //  drop the db and deletes the files on disk
            var script2 = "DROP DATABASE NorthwindTest;"; 
            RunSqlOnMaster(script1);
            RunSqlOnMaster(script2);
        }

        private static void RunSqlOnMaster(string script)
        {
            using (var connection = new SqlConnection(sqlConnectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
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

        public TestContext TestContext { get; set; }
    }
}
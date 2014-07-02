using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Northwind.Entities.Models;
using Northwind.Repository.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Northwind.Repository.Repositories;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestInitialize]
        public void SettingUpNorthwindTestDatabase()
        {
            TestContext.WriteLine("Please copy //Sql//instnwnd.sql is copied to C:\\temp\\instnwnd.sql for test to run succesfully");
            TestContext.WriteLine("TestFixture executing, creating NorthwindTest Db for intergration  tests");
            const string sqlConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True";
            TestContext.WriteLine("Loading and parsing create NorthwindTest database Sql script");
            var file = new FileInfo("C:\\temp\\instnwnd.sql");
            var script = file.OpenText().ReadToEnd();
            var connection = new SqlConnection(sqlConnectionString);
            var server = new Server(new ServerConnection(connection));
            server.ConnectionContext.ExecuteNonQuery(script);
            TestContext.WriteLine("NorthwindTest Db created for intergration tests");
        }

        [TestCleanup]
        public void Cleanup()
        {
            //TODO: delete NorthwindTest database
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

        public TestContext TestContext { get; set; }
    }
}
using System.IO;
using System.Linq;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using Northwind.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Northwind.Repository.Repositories;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class CustomerTests
    {
        [TestInitialize]
        public void Setup()
        {
            const string sqlConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True";
            var file = new FileInfo("C:\\temp\\instnwnd.sql");
            var script = file.OpenText().ReadToEnd();
            var connection = new SqlConnection(sqlConnectionString);
            var server = new Server(new ServerConnection(connection));
            server.ConnectionContext.ExecuteNonQuery(script);
        }

        [TestCleanup]
        public void Cleanup()
        {
            //TODO: delete NorthwindTest database
        }

        [TestMethod]
        public void TestMethod1()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<Customer> customerRepository = new Repository<Customer>(context, unitOfWork);
                var customerOrders = customerRepository.GetCustomerOrder("USA");
                Assert.IsTrue(customerOrders.Any());
            }
        }
    }
}
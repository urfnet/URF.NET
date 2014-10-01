using System;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class ProductRepositoryTest
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

        private static void RunSqlOnMaster(string script)
        {
            using (var connection = new SqlConnection(MasterConnectionString))
            {
                var server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        [TestMethod]
        public void InsertProducts()
        {
            using (IDataContextAsync context = new NorthwindContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, _repositoryProvider))
            {
                IRepositoryAsync<Product> productRepository = new Repository<Product>(context, unitOfWork);

                var newProducts = new[]
                {
                    new Product {ProductName = "One", Discontinued = false, ObjectState = ObjectState.Added},
                    new Product {ProductName = "12345678901234567890123456789012345678901234567890", Discontinued = true, ObjectState = ObjectState.Added},
                    new Product {ProductName = "Three", Discontinued = true, ObjectState = ObjectState.Added},
                    new Product {ProductName = "Four", Discontinued = true, ObjectState = ObjectState.Added},
                    new Product {ProductName = "Five", Discontinued = true, ObjectState = ObjectState.Added}
                };

                foreach (var product in newProducts)
                {
                    try
                    {
                        productRepository.Insert(product);
                        unitOfWork.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var sb = new StringBuilder();

                        foreach (var failure in ex.EntityValidationErrors)
                        {
                            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());

                            foreach (var error in failure.ValidationErrors)
                            {
                                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                                sb.AppendLine();
                            }
                        }

                        Debug.WriteLine(sb.ToString());
                        TestContext.WriteLine(sb.ToString());
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        TestContext.WriteLine(ex.Message);
                    }
                }

                var insertedProduct = productRepository.Query(x => x.ProductName == "One").Select().FirstOrDefault();
                Assert.IsTrue(insertedProduct.ProductName == "One");
            }
        }
    }
}

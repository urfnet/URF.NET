using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Test.IntegrationTests
{
    /// <summary>
    /// Summary description for ChangeTrackerEntries
    /// </summary>
    [TestClass]
    public class ChangeTrackerEntries
    {
        private TestContext testContextInstance;

        [TestInitialize]
        public void Initialize()
        {
            //Utility.CreateSeededTestDatabase();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestMethod]
        public void AddProducts()
        {
            for (var x = 0; x < 2; x++)
            {
                var products = new List<Product>();

                for (var i = 0; i < 100; i++)
                {
                    products.Add(new Product
                    {
                        ProductName = Guid.NewGuid().ToString(),
                        Discontinued = false,
                        ObjectState = ObjectState.Added
                    });
                }

                using (IDataContextAsync context = new NorthwindContext())
                using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context))
                {
                    var northwindContext = (NorthwindContext)context;
                    Assert.IsFalse(northwindContext.ChangeTracker.Entries().Any());

                    IRepositoryAsync<Product> productRepository =
                        new Repository<Product>(context, unitOfWork);

                    productRepository.InsertGraphRange(products);
                    products.Clear();
                    unitOfWork.SaveChanges();

                    Assert.IsTrue(northwindContext.ChangeTracker.Entries().Count() == 100);
                }

                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}

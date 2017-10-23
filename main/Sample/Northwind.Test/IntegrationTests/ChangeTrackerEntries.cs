using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;

namespace Northwind.Test.IntegrationTests
{
    /// <summary>
    /// Summary description for ChangeTrackerEntries
    /// </summary>
    [TestClass]
    public class ChangeTrackerEntries
    {
        [TestInitialize]
        public void Initialize()
        {
            //Utility.CreateSeededTestDatabase();
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
                        TrackingState = TrackingState.Added
                    });
                }

                using (var context = new NorthwindContext())
                {
                    IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);
                    var northwindContext = (NorthwindContext) context;
                    Assert.IsFalse(northwindContext.ChangeTracker.Entries().Any());

                    IRepositoryAsync<Product> productRepository =
                        new Repository<Product>(context, unitOfWork);

                    productRepository.InsertRange(products);
                    products.Clear();
                    unitOfWork.SaveChanges();

                    Assert.IsTrue(northwindContext.ChangeTracker.Entries().Count() == 100);
                }

                System.Threading.Thread.Sleep(5000);
            }
        }
    }
}

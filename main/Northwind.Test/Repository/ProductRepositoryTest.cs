#region

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Data.Models;
using Northwind.Test.Fake;
using Repository;
using Repository.Providers.EntityFramework;

#endregion

namespace Northwind.Test.Repository
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            //todo: setup actual NorthwindFake.mdf
            // copy ProductRepositoryTests.cs and run all tests against actual NorthwindContct instead of NorthwindFakeContext.cs
            // run all testes with actual NorthwindTest.mdf (LocalDb) NOT the NorthwindFakeContext.cs
        }

        [TestCleanup]
        public void TestCleanup()
        {
            // todo: delete NorthwindTest.mfd (LocalDb)
            // cleanup all the infrastructure that was needed for our tests.
        }

        [TestMethod]
        public void DeleteProductById()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.Save();

                unitOfWork.Repository<Product>().Delete(2);

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNull(product);
            }
        }

        [TestMethod]
        public void DeepLoadProductWithSupplier()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Supplier>().Insert(new Supplier {SupplierID = 1, CompanyName = "Nokia", City = "Tampere", Country = "Finland", ContactName = "Stephen Elop", ContactTitle = "CEO"});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ProductName = "Nokia Lumia 1520", SupplierID = 1, ObjectState = ObjectState.Added});

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
            }
        }

        [TestMethod]
        public void DeleteProductByProduct()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                product.ObjectState = ObjectState.Deleted;

                unitOfWork.Repository<Product>().Delete(product);

                unitOfWork.Save();

                var productDeleted = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNull(productDeleted);
            }
        }

        [TestMethod]
        public void FindProductById()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true});

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
                Assert.AreEqual(2, product.ProductID);
            }
        }

        [TestMethod]
        public void GetProductsExecutesQuery()
        {
            using (IDataContext context = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var products = unitOfWork.Repository<Product>().Query().Get().ToList();
                Assert.IsInstanceOfType(products, typeof (List<Product>));
            }
        }

        [TestMethod]
        public void GetProductsThatHaveBeenDiscontinued()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.Save();

                var discontinuedProducts = unitOfWork.Repository<Product>().Query().Filter(t => t.Discontinued).Get();

                Assert.AreEqual(2, discontinuedProducts.Count());
            }
        }

        [TestMethod]
        public void InsertProduct()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
                Assert.AreEqual(2, product.ProductID);
            }
        }

        [TestMethod]
        public void InsertRangeOfProducts()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                var newProducts = new[]
                {
                    new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added},
                    new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added},
                    new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added}
                };

                unitOfWork.Repository<Product>().InsertRange(newProducts);

                var savedProducts = unitOfWork.Repository<Product>().Query().Get();

                Assert.AreEqual(savedProducts.Count(), newProducts.Length);
            }
        }

        [TestMethod]
        public void UpdateProduct()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true});

                unitOfWork.Save();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.AreEqual(product.Discontinued, true, "Assert we are able to find the inserted Product.");

                product.Discontinued = false;
                product.ObjectState = ObjectState.Modified;

                unitOfWork.Repository<Product>().Update(product);
                unitOfWork.Save();

                Assert.AreEqual(product.Discontinued, false, "Assert that our changes were saved.");
            }
        }

        [TestMethod]
        public async void FindProductKeyAsync()
        {
            using (IDataContext northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true});

                unitOfWork.Save();

                var product = await unitOfWork.Repository<Product>().FindAsync(2);

                Assert.AreEqual(product.ProductID, 2);
            }
        }
    }
}
#region

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Northwind.Test.Fake;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Test.UnitTests.Repository
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
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
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                unitOfWork.Repository<Product>().Delete(2);

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNull(product);
            }
        }

        [TestMethod]
        public void DeepLoadProductWithSupplier()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Supplier>().Insert(new Supplier {SupplierID = 1, CompanyName = "Nokia", City = "Tampere", Country = "Finland", ContactName = "Stephen Elop", ContactTitle = "CEO", ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ProductName = "Nokia Lumia 1520", SupplierID = 1, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
            }
        }

        [TestMethod]
        public void DeleteProductByProduct()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                product.ObjectState = ObjectState.Deleted;

                unitOfWork.Repository<Product>().Delete(product);

                unitOfWork.SaveChanges();

                var productDeleted = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNull(productDeleted);
            }
        }

        [TestMethod]
        public void FindProductById()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
                Assert.AreEqual(2, product.ProductID);
            }
        }

        [TestMethod]
        public void GetProductsExecutesQuery()
        {
            using (IDataContextAsync context = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(context))
            {
                var products = unitOfWork.Repository<Product>().Query().Select().ToList();
                Assert.IsInstanceOfType(products, typeof (List<Product>));
            }
        }

        [TestMethod]
        public void GetProductsThatHaveBeenDiscontinued()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var discontinuedProducts = unitOfWork.Repository<Product>().Query(t => t.Discontinued).Select();

                Assert.AreEqual(2, discontinuedProducts.Count());
            }
        }

        [TestMethod]
        public void InsertProduct()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.IsNotNull(product);
                Assert.AreEqual(2, product.ProductID);
            }
        }

        [TestMethod]
        public void InsertRangeOfProducts()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                var newProducts = new[]
                {
                    new Product {ProductID = 1, Discontinued = false, ObjectState = ObjectState.Added},
                    new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added},
                    new Product {ProductID = 3, Discontinued = true, ObjectState = ObjectState.Added}
                };

                unitOfWork.Repository<Product>().InsertRange(newProducts);

                var savedProducts = unitOfWork.Repository<Product>().Query().Select();

                Assert.AreEqual(savedProducts.Count(), newProducts.Length);
            }
        }

        [TestMethod]
        public void UpdateProduct()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWork unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true, ObjectState = ObjectState.Added});

                unitOfWork.SaveChanges();

                var product = unitOfWork.Repository<Product>().Find(2);

                Assert.AreEqual(product.Discontinued, true, "Assert we are able to find the inserted Product.");

                product.Discontinued = false;
                product.ObjectState = ObjectState.Modified;

                unitOfWork.Repository<Product>().Update(product);
                unitOfWork.SaveChanges();

                Assert.AreEqual(product.Discontinued, false, "Assert that our changes were saved.");
            }
        }

        [TestMethod]
        public async void FindProductKeyAsync()
        {
            using (IDataContextAsync northwindFakeContext = new NorthwindFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(northwindFakeContext))
            {
                unitOfWork.Repository<Product>().Insert(new Product {ProductID = 2, Discontinued = true});

                unitOfWork.SaveChanges();

                var product = await unitOfWork.RepositoryAsync<Product>().FindAsync(2);

                Assert.AreEqual(product.ProductID, 2);
            }
        }
    }
}
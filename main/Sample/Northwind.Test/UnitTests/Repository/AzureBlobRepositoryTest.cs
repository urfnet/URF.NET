#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Northwind.Test.Fake;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Test.UnitTests.Repository
{
    /// <summary>
    /// Unit test for "custom" repositories support with psuedo AzureBlobRepository
    /// </summary>    
    [TestClass]
    public class AzureBlobRepositoryTest
    {
        /// <summary>
        /// Least preferred way of retrieving a custom repository
        /// </summary>
        [TestMethod]
        public void GetCustomRepositoryReturnsCustomRepository()
        {
            //  Arrange
            var factories = new Dictionary<Type, Func<dynamic>>
            {
                {
                    typeof (IAzureBlobRepository<MyBlob>),
                    () => new AzureBlobRepository<MyBlob>()
                }
            };

            var repositoryFactories = new RepositoryFactories(factories);

            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(null, new RepositoryProvider(repositoryFactories)))
            {
                //  Act
                var azureBlobRepository =
                    unitOfWork.GetCustomRepository(typeof (IAzureBlobRepository<MyBlob>));

                //  Assert                        
                Assert.AreEqual(azureBlobRepository.SayHello(), "Hello World!");
            }
        }

        /// <summary>
        /// Preferred way of retrieving custom repository
        /// </summary>
        [TestMethod]
        public void GetCustomRepositoryGenericVersionReturnsCustomRepository()
        {
            //  Arrange
            var customRepositoryFactories = new Dictionary<Type, Func<dynamic>>
            {
                {
                    typeof (IAzureBlobRepository<MyBlob>),
                    () => new AzureBlobRepository<MyBlob>()
                }
            };

            var repositoryFactories = new RepositoryFactories(customRepositoryFactories);

            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(null, new RepositoryProvider(repositoryFactories)))
            {
                //  Act
                var azureBlobRepository =
                    unitOfWork.GetCustomRepository<IAzureBlobRepository<MyBlob>>();

                //  Assert                        
                Assert.AreEqual(azureBlobRepository.SayHello(), "Hello World!");
            }
        }

        /// <summary>
        /// Retrieving custom repository from another repository, this is useful for when creating a custom
        /// query and needing access to other repositories.
        /// </summary>
        [TestMethod]
        public void GetCustomRepositoryFromOtherRepository()
        {
            //  Arrange
            var factories = new Dictionary<Type, Func<dynamic>>
            {
                {
                    typeof (IAzureBlobRepository<MyBlob>),
                    () => new AzureBlobRepository<MyBlob>()
                }
            };

            IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories(factories));

            using (IDataContextAsync context = new NorthwindFakeContext())
            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, repositoryProvider))
            {
                IRepositoryAsync<Product> productRepository = new Repository<Product>(context, unitOfWork);
                var azureBlobRepository = productRepository.GetCustomRepository<IAzureBlobRepository<MyBlob>>();

                Assert.AreEqual(azureBlobRepository.SayHello(), "Hello World!");
            }
        }

        private class AzureBlobRepository<T> : IAzureBlobRepository<T>
        {
            public string SayHello()
            {
                return "Hello World!";
            }

            // Implementing IObjectState, however not used in this use case
            public ObjectState ObjectState { get; set; }
        }

        internal interface IAzureBlobRepository<T>: IObjectState
        {
            string SayHello();
        }

        private class MyBlob
        {
        }
    }
}
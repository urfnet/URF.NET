#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.UnitOfWork;

#endregion

namespace Northwind.Test.UnitTests.Repository
{
    [TestClass]
    public class CustomRepositoryTest
    {
        // Least preferred way of retrieving custom repository
        [TestMethod]
        public void GetCustomRepositoryReturnsCustomRepository()
        {
            //  Arrange
            var customRepositoryFactories = new Dictionary<Type, Func<dynamic>> {{typeof (IAzureBlobRepository<Blob>), () => new MyCustomRepository<Blob>()}};
            var repositoryFactories = new RepositoryFactories(customRepositoryFactories);

            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(null, new RepositoryProvider(repositoryFactories)))
            {
                //  Act
                var azureBlobRepository = unitOfWork.GetCustomRepository(typeof (IAzureBlobRepository<Blob>));

                //  Assert                        
                Assert.AreEqual(azureBlobRepository.SayHello(), "Hello all");
            }
        }

        // Preferred way of retrieving custom repository
        [TestMethod]
        public void GetCustomRepositoryGenericVersionReturnsCustomRepository()
        {
            //  Arrange
            var customRepositoryFactories = new Dictionary<Type, Func<dynamic>> {{typeof (IAzureBlobRepository<Blob>), () => new MyCustomRepository<Blob>()}};
            var repositoryFactories = new RepositoryFactories(customRepositoryFactories);

            using (IUnitOfWorkAsync unitOfWork = new UnitOfWork(null, new RepositoryProvider(repositoryFactories)))
            {
                //  Act
                var azureBlobRepository = unitOfWork.GetCustomRepository<IAzureBlobRepository<Blob>>();

                //  Assert                        
                Assert.AreEqual(azureBlobRepository.SayHello(), "Hello all");
            }
        }

        private class Blob
        {
        }

        private class MyCustomRepository<T> : IAzureBlobRepository<T>
        {
            public string SayHello()
            {
                return "Hello all";
            }
        }
    }

    internal interface IAzureBlobRepository<T>
    {
        string SayHello();
    }
}
using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.UnitOfWork;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class UnitOfWork_Tests
    {
        [TestMethod]
        public void UnitOfWork_Dispose_Test()
        {
            IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
            IDataContextAsync context = new NorthwindContext();
            IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, repositoryProvider);

            // opening connection
            unitOfWork.BeginTransaction();
            unitOfWork.Commit();

            // calling dispose 1st time
            unitOfWork.Dispose();
            var isDisposed = (bool) GetInstanceField(typeof (UnitOfWork), unitOfWork, "_disposed");
            Assert.IsTrue(isDisposed);

            // calling dispose 2nd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();

            // calling dispose 3rd time, should not throw any excpetions
            context.Dispose();
            unitOfWork.Dispose();
        }

        [TestMethod]
        public void IDataContext_Dispose_Test()
        {
            IRepositoryProvider repositoryProvider = new RepositoryProvider(new RepositoryFactories());
            IDataContextAsync context = new NorthwindContext();
            IUnitOfWorkAsync unitOfWork = new UnitOfWork(context, repositoryProvider);

            // opening connection
            unitOfWork.BeginTransaction();
            unitOfWork.Commit();

            // calling dispose 1st time
            context.Dispose();

            var isDisposed = (bool) GetInstanceField(typeof (DataContext), context, "_disposed");
            Assert.IsTrue(isDisposed);

            // calling dispose 2nd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();

            // calling dispose 3rd time, should not throw any excpetions
            unitOfWork.Dispose();
            context.Dispose();
        }

        private static object GetInstanceField(Type type, object instance, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
            var field = type.GetField(fieldName, bindFlags);
            return field != null ? field.GetValue(instance) : null;
        }
    }
}
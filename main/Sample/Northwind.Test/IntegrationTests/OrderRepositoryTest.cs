using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Northwind.Entities.Models;
using Repository.Pattern.Ef6;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using TrackableEntities;

namespace Northwind.Test.IntegrationTests
{
    [TestClass]
    public class OrderRepositoryTest
    {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Utility.CreateSeededTestDatabase();
        }

        [TestMethod]
        public void CreateOrderObjectGraphTest()
        {
            using (var context = new NorthwindContext())
            {
                IUnitOfWorkAsync unitOfWork = new UnitOfWork(context);
                IRepositoryAsync<Order> orderRepository = new Repository<Order>(context, unitOfWork);

                var orderTest = new Order
                {
                    CustomerID = "LLE39",
                    EmployeeID = 10,
                    OrderDate = DateTime.Now,
                    TrackingState = TrackingState.Added,

                    Employee = new Employee
                    {
                        EmployeeID = 10,
                        FirstName = "Test",
                        LastName = "Le",
                        TrackingState = TrackingState.Added
                    },

                    OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail
                        {
                            ProductID = 1,
                            Quantity = 5,
                            TrackingState = TrackingState.Added
                        },
                        new OrderDetail
                        {
                            ProductID = 2,
                            Quantity = 5,
                            TrackingState = TrackingState.Added
                        }
                    }
                };

                //orderRepository.UpsertGraph(orderTest);

                try
                {
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
        }
    }
}
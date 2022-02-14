using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefactoringChallenge.Business.DTO;
using RefactoringChallenge.Business.Exceptions;
using RefactoringChallenge.Business.Services;
using RefactoringChallenge.Data.Enities;
using RefactoringChallenge.Test.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringChallenge.Test
{
    [TestClass]
    public class OrderServiceTest : TestBase
    {
        [TestMethod]
        public void OrderService_GetPagingResult_ShouldReturnAllItemsByDefault()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Orders.Add(new Order { OrderId = 1 });
                db.Orders.Add(new Order { OrderId = 2 });
                db.Orders.Add(new Order { OrderId = 3 });
                db.Orders.Add(new Order { OrderId = 4 });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);

                //Action
                var result = orderService.GetPagingResult();

                //Assert
                Assert.AreEqual(4, result.PageData.Count());
                Assert.AreEqual(1, result.PageData[0].OrderId);
                Assert.AreEqual(2, result.PageData[1].OrderId);
                Assert.AreEqual(3, result.PageData[2].OrderId);
                Assert.AreEqual(4, result.PageData[3].OrderId);
            }
        }

        [TestMethod]
        public void OrderService_GetPagingResult_ShouldReturnFilteredItems()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Orders.Add(new Order { OrderId = 1 });
                db.Orders.Add(new Order { OrderId = 2 });
                db.Orders.Add(new Order { OrderId = 3 });
                db.Orders.Add(new Order { OrderId = 4 });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);

                //Action
                var result = orderService.GetPagingResult(1, 2);

                //Assert
                Assert.AreEqual(2, result.PageData.Count());
                Assert.AreEqual(2, result.PageData[0].OrderId);
                Assert.AreEqual(3, result.PageData[1].OrderId);
            }
        }

        [TestMethod]
        public void OrderService_GetPagingResult_ShouldReturn3Items()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Orders.Add(new Order { OrderId = 1 });
                db.Orders.Add(new Order { OrderId = 2 });
                db.Orders.Add(new Order { OrderId = 3 });
                db.Orders.Add(new Order { OrderId = 4 });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);

                //Action
                var result = orderService.GetPagingResult(1);

                //Assert
                Assert.AreEqual(3, result.PageData.Count());
                Assert.AreEqual(2, result.PageData[0].OrderId);
                Assert.AreEqual(3, result.PageData[1].OrderId);
                Assert.AreEqual(4, result.PageData[2].OrderId);
            }
        }

        [TestMethod]
        public void OrderService_Delete_ShouldBeSuccess()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Products.Add(new Product { ProductId = 1 });
                db.OrderDetails.Add(new OrderDetail { OrderId = 1, ProductId = 1 });
                db.Orders.Add(new Order { OrderId = 1 });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);

                var orderId = 1;

                //Action
                var result = orderService.Delete(orderId);

                //Assert
                Assert.AreEqual(orderId, result);
                Assert.AreEqual(0, db.OrderDetails.Count());
                Assert.AreEqual(0, db.Orders.Count());
            }
        }

        [TestMethod]
        public void OrderService_GetById_ShouldReturnOrderDetails()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Orders.Add(new Order
                {
                    OrderId = 1,
                    CustomerId = "C1",
                    EmployeeId = 2,
                    Freight = 3,
                    RequiredDate = new DateTime(2022, 2, 12),
                    OrderDate = new DateTime(2022, 2, 12),
                    ShipPostalCode = "A",
                    ShipAddress = "B",
                    ShipCity = "C",
                    ShipCountry = "D",
                    ShipName = "E",
                    ShippedDate = new DateTime(2022, 2, 12),
                    ShipVia = 4,
                    ShipRegion = "F"
                });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);
                var orderId = 1;

                //Action
                var result = orderService.GetById(orderId);

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.OrderId);
                Assert.AreEqual("C1", result.CustomerId);
                Assert.AreEqual(2, result.EmployeeId);
                Assert.AreEqual(3, result.Freight);
                Assert.AreEqual(new DateTime(2022, 2, 12), result.RequiredDate);
                Assert.AreEqual(new DateTime(2022, 2, 12), result.OrderDate);
                Assert.AreEqual("A", result.ShipPostalCode);
                Assert.AreEqual("B", result.ShipAddress);
                Assert.AreEqual("C", result.ShipCity);
                Assert.AreEqual("D", result.ShipCountry);
                Assert.AreEqual("E", result.ShipName);
                Assert.AreEqual(new DateTime(2022, 2, 12), result.ShippedDate);
                Assert.AreEqual(4, result.ShipVia);
                Assert.AreEqual("F", result.ShipRegion);
            }
        }


        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundExeption))]
        public void OrderService_GetById_ShouldThrowExceptionIfNotExist()
        {
            using (var db = CreateContext())
            {
                var orderService = new OrderService(db, mapper);
                var result = orderService.GetById(1);
            }
        }

        [TestMethod]
        public void OrderService_AddProductsToOrder_ShouldCreatePrductItemsInOrder()
        {
            //Arrange
            using (var db = CreateContext())
            {
                db.Products.Add(new Product { ProductId = 1 });
                db.Products.Add(new Product { ProductId = 2 });
                db.Orders.Add(new Order { OrderId = 1 });
                db.SaveChanges();

                var orderService = new OrderService(db, mapper);

                //Action
                var result = orderService.AddProductsToOrder(1, new List<OrderDetailRequest>()
                {
                    new OrderDetailRequest{ Discount = 1, ProductId = 1, Quantity = 1, UnitPrice = 1},
                    new OrderDetailRequest{ Discount = 2, ProductId = 2, Quantity = 2, UnitPrice = 2},
                });

                //Assert
                Assert.AreEqual(2, result.Count());

                Assert.AreEqual(1, result.ElementAt(0).OrderId);
                Assert.AreEqual(1, result.ElementAt(1).OrderId);

                Assert.AreEqual(1, result.ElementAt(0).Discount);
                Assert.AreEqual(1, result.ElementAt(0).ProductId);
                Assert.AreEqual(1, result.ElementAt(0).Quantity);
                Assert.AreEqual(1, result.ElementAt(0).UnitPrice);

                Assert.AreEqual(2, result.ElementAt(1).Discount);
                Assert.AreEqual(2, result.ElementAt(1).ProductId);
                Assert.AreEqual(2, result.ElementAt(1).Quantity);
                Assert.AreEqual(2, result.ElementAt(1).UnitPrice);

                Assert.AreEqual(2, db.OrderDetails.Count());
                Assert.AreEqual(1, db.Orders.Count());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(EntityNotFoundExeption))]
        public void OrderService_AddProductsToOrder_ShouldThrowExceptionIfOrderNotExists()
        {
            //Arrange
            using (var db = CreateContext())
            {
                var orderService = new OrderService(db, mapper);

                //Action
                orderService.AddProductsToOrder(1, new List<OrderDetailRequest>()
                {
                    new OrderDetailRequest{ Discount = 1, ProductId = 1, Quantity = 1, UnitPrice = 1},
                    new OrderDetailRequest{ Discount = 2, ProductId = 2, Quantity = 2, UnitPrice = 2},
                });
            }
        }


        [TestMethod]
        public void OrderService_Create_ShouldCreateANewOrder()
        {
            //Arrange
            using (var db = CreateContext())
            {
                var orderService = new OrderService(db, mapper);

                //Action
                var result = orderService.Create(new CreateOrderRequest
                {
                    CustomerId = "A",
                    EmployeeId = 1,
                    Freight = 2,
                    OrderDetails = new List<OrderDetailRequest>()
                    {
                        new OrderDetailRequest()
                        {
                            Discount = 1,
                            ProductId = 2,
                            Quantity = 3,
                            UnitPrice = 4
                        }
                    },
                    RequiredDate = new DateTime(2022, 2, 12),
                    ShipAddress = "B",
                    ShipCity = "C",
                    ShipCountry = "D",
                    ShipName = "E",
                    ShipPostalCode = "F",
                    ShipRegion = "G",
                    ShipVia = 3
                });

                //Assert
                Assert.IsNotNull(result);
                Assert.AreEqual("A", result.CustomerId);
                Assert.AreEqual(1, result.EmployeeId);
                Assert.AreEqual(2, result.Freight);
                Assert.AreEqual(DateTime.Now.Date, result.OrderDate?.Date);
                Assert.AreEqual(new DateTime(2022, 2, 12), result.RequiredDate);
                Assert.AreEqual("B", result.ShipAddress);
                Assert.AreEqual("C", result.ShipCity);
                Assert.AreEqual("D", result.ShipCountry);
                Assert.AreEqual("E", result.ShipName);
                Assert.AreEqual("F", result.ShipPostalCode);
                Assert.AreEqual("G", result.ShipRegion);
                Assert.AreEqual(3, result.ShipVia);

                Assert.AreEqual(1, result.OrderDetails.Count());
                Assert.AreEqual(1, result.OrderDetails.ElementAt(0).Discount);
                Assert.AreEqual(2, result.OrderDetails.ElementAt(0).ProductId);
                Assert.AreEqual(3, result.OrderDetails.ElementAt(0).Quantity);
                Assert.AreEqual(4, result.OrderDetails.ElementAt(0).UnitPrice);
            }
        }
    }
}


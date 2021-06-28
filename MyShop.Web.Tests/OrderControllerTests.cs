using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyShop.Domain.Models;
using MyShop.Infrastructure.Repositories;
using MyShop.Web.Controllers;
using MyShop.Web.Models;
using System;

namespace MyShop.Web.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        /* You can create mock (fake) version of your interfaces in your tests 
            using framework such as Moq */

        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            var orderRepository = new Mock<IRepository<Order>>();
            var productRepository = new Mock<IRepository<Product>>();

            // Mock<IRepository<T>> allows us to write fake behaviour that acts like a real repository
            var orderController = new OrderController(
                orderRepository.Object,
                productRepository.Object
            );

            var createOrderModel = new CreateOrderModel
            {
                Customer = new CustomerModel
                {
                    Name = "Filip Ekberg",
                    ShippingAddress = "Test address",
                    City = "Gothenburg",
                    PostalCode = "43317",
                    Country = "Sweden"
                },
                LineItems = new[]
                {
                new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 2 },
                new LineItemModel { ProductId = Guid.NewGuid(), Quantity = 12 }
                }
            };

            orderController.Create(createOrderModel);

            orderRepository.Verify(respository => respository.Add(It.IsAny<Order>()),
                Times.AtMostOnce());
        }
    }
}

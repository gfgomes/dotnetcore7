﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            // Arrange - create a mock repository
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create an empty cart
            Cart cart = new Cart();
            // Arrange - create the order
            Order order = new Order();
            // Arrange - create an instance of the controller 
            OrderController target =
                new OrderController(mock.Object, cart);



            // Act
            ViewResult? result = target.Checkout(order) as ViewResult;

            // Assert - check that the order hasn't been stored 
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            // Assert - check that the method is returning the default view 
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            // Assert - check I am passing an invalid model to the view 
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange - create a mock order repository 
            Mock<IOrderRepository> mock = new Mock<IOrderRepository>();
            // Arrange - create a cart with one item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);
            // Arrange - create an instance of the controller
            OrderController target = new OrderController(mock.Object, cart);

            // Act - try to checkout 
            RedirectToPageResult? result =
                    target.Checkout(new Order()) as RedirectToPageResult;

            // Assert - check that the order has been stored
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Once);
            // Assert - check the method is redirecting to the Completed action 
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}

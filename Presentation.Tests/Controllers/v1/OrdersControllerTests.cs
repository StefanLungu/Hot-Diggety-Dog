using Application.Features.OrderFeatures.Services;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository.v1;
using System;
using System.Collections.Generic;
using WebApi.Controllers.v1;
using Xunit;

namespace Presentation.Tests.Controllers.v1
{
    [Collection("Sequential")]
    public class OrdersControllerTests : DatabaseBaseTest
    {
        private readonly OrdersController _ordersController;

        public OrdersControllerTests()
        {
            OrdersRepository orderRepository = new(dataContext);
            UsersRepository userRepository = new(dataContext);
            Repository<OrderProduct> orderPorductRepository = new(dataContext);
            Repository<Product> productsRepository = new(dataContext);
            OrdersService ordersService = new();

            _ordersController = new OrdersController(orderRepository, userRepository, orderPorductRepository,
                                                        productsRepository, ordersService);
        }

        [Fact]
        public async void GetOrdersByCustomerId_WhenCustomerDoesntExists_ShouldReturn_NotFound()
        {
            //Arrange
            Guid customerId = Guid.Parse("6144f36f-3b31-4e74-984e-43e549351948");
            PaginationDto pagination = new()
            {
                Page = 1,
                EntitiesPerPage = 3
            };

            // Act
            ActionResult<IEnumerable<Order>> actionResult = await _ordersController.GetOrdersByCustomerId(customerId, pagination);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetOrdersByOperatorId_WhenOperatorDoesntExists_ShouldReturn_NotFound()
        {
            //Arrange
            Guid operatorId = Guid.Parse("6144f36f-3b31-4e74-984e-43e549351948");
            PaginationDto pagination = new()
            {
                Page = 1,
                EntitiesPerPage = 3
            };

            // Act
            ActionResult<IEnumerable<Order>> actionResult = await _ordersController.GetOrdersByOperatorId(operatorId, pagination);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void GetOrderBy_Generated_Id_ShouldReturn_NotFound()
        {
            //Arrange
            Guid id = Guid.Parse("8d63df60-fa0d-40a0-9a98-6ace7ea6db43");

            // Act
            ActionResult<Order> actionResult = await _ordersController.GetOrderById(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_Null_Order_ShouldReturn_BadRequest()
        {
            //Arrange
            CreateOrderRequest createOrderRequest = null;

            // Act
            ActionResult<Order> actionResult = await _ordersController.CreateOrder(createOrderRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_Order_With_Null_Operator_ShouldReturn_NotFound()
        {
            //Arrange
            CreateOrderRequest createOrderRequest = new()
            {
                OperatorId = Guid.Empty,
                UserId = Guid.Parse("d9500fbb-0b51-4a1d-9e65-dd88dd7389ee"),
                Products = null,
                Timestamp = DateTime.Now
            };

            // Act
            ActionResult<Order> actionResult = await _ordersController.CreateOrder(createOrderRequest);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

        [Fact]
        public async void Create_Order_With_Null_Customer_ShouldReturn_NotFound()
        {
            //Arrange
            CreateOrderRequest createOrderRequest = new()
            {
                OperatorId = Guid.Parse("d9605834-2d64-416c-9e33-af9cc5c04735"),
                UserId = Guid.Empty,
                Products = null,
                Timestamp = DateTime.Now
            };

            // Act
            ActionResult<Order> actionResult = await _ordersController.CreateOrder(createOrderRequest);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }
    }
}

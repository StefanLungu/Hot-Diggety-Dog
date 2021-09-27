using Application.Features.OrderFeatures.Commands;
using Application.Features.OrderFeatures.Queries;
using Application.Features.OrderFeatures.Qureries;
using Application.Features.OrderFeatures.Services;
using Application.Interfaces;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using WebApi.Controllers.v2;
using Xunit;

namespace Presentation.Tests.Controllers.v2
{
    public class OrdersControllerTests : DatabaseBaseTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IOrdersService _ordersService;

        public OrdersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _ordersService = new OrdersService();
        }

        [Fact]
        public void Mediatr_GetOrder_By_NonExisting_Order_Id_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderByIdQuery>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            Guid orderId = Guid.Parse("bb247353-b753-421c-8e8d-20406486087c");


            //Act
            var result = (ordersController.GetOrderById(orderId).Result).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetOrders_By_NonExisting_Customer_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetOrderByIdQuery>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            PaginationDto paginationDto = new()
            {
                EntitiesPerPage = 10,
                Page = 1
            };
            Guid customerId = Guid.Parse("802767ab-785a-4c15-b592-dd78870906e6");

            //Act
            var result = (ordersController.GetOrdersByCustomerId(customerId, paginationDto, new OrderFilterDto()).Result).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_GetOrders_By_NonExisting_Operator_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetOrdersByOperatorIdQuery>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            PaginationDto paginationDto = new()
            {
                EntitiesPerPage = 10,
                Page = 1
            };
            Guid operatorId = Guid.Parse("564e2879-a683-4a19-8e89-bf632e0aec0d");

            //Act
            var result = (ordersController.GetOrdersByOperatorId(operatorId, paginationDto, new OrderFilterDto()).Result).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Null_Order_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            CreateOrderRequest orderRequest = null;

            //Act
            var result = ordersController.CreateOrder(orderRequest).Result;

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Order_With_Null_Operator_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            CreateOrderRequest orderRequest = new()
            {
                OperatorId = Guid.Empty,
                UserId = Guid.Parse("d9500fbb-0b51-4a1d-9e65-dd88dd7389ee"),
                Products = null,
                Timestamp = DateTime.Now
            };

            //Act
            var result = ordersController.CreateOrder(orderRequest).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void Mediatr_Create_Order_With_Null_Customer_ShouldReturn_NotFound()
        {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), new System.Threading.CancellationToken()));
            var ordersController = new OrdersController(_mediator.Object, _ordersService);
            CreateOrderRequest orderRequest = new()
            {
                OperatorId = Guid.Parse("d9605834-2d64-416c-9e33-af9cc5c04735"),
                UserId = Guid.Empty,
                Products = null,
                Timestamp = DateTime.Now
            };

            //Act
            var result = ordersController.CreateOrder(orderRequest).Result;

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}

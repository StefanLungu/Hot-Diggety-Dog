using Application.Features.OrderFeatures.Commands;
using Application.Features.OrderFeatures.Queries;
using Application.Features.OrderFeatures.Qureries;
using Application.Features.OrderProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using Application.Features.UserFeatures.Queries;
using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Security.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Extensions;
using WebApi.Resources;
using WebAPI.Controllers;

namespace WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class OrdersController : BaseApiController
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IMediator mediator, IOrdersService orderService) : base(mediator)
        {
            _ordersService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] PaginationDto pagination, [FromQuery] OrderFilterDto filter)
        {
            var queryable = await mediator.Send(new GetOrdersQuery());
            queryable = _ordersService.Filter(queryable, filter);
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("customers/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(Guid customerId, [FromQuery] PaginationDto pagination, [FromQuery] OrderFilterDto filter)
        {
            User customerUser = await mediator.Send(new GetUserByIdQuery() { Id = customerId });
            if (customerUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, customerId));
            }

            if (customerUser.Role != Role.CUSTOMER)
            {
                return BadRequest(Messages.InvalidData);
            }

            var queryable = await mediator.Send(new GetOrdersByCustomerIdQuery() { Id = customerId });
            queryable = _ordersService.Filter(queryable, filter);
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("operators/{operatorId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByOperatorId(Guid operatorId, [FromQuery] PaginationDto pagination, [FromQuery] OrderFilterDto filter)
        {
            User operatorUser = await mediator.Send(new GetUserByIdQuery() { Id = operatorId });

            if (operatorUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, operatorId));
            }

            if (operatorUser.Role != Role.OPERATOR)
            {
                return BadRequest(Messages.InvalidData);
            }

            var queryable = await mediator.Send(new GetOrdersByOperatorIdQuery() { Id = operatorId });
            queryable = _ordersService.Filter(queryable, filter);
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            Order order = await mediator.Send(new GetOrderByIdQuery() { Id = id });

            if (order == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.OrderEntity, id));
            }

            return Ok(await mediator.Send(new GetOrderByIdQuery() { Id = id }));
        }
        

        [HttpGet("discount/{id}")]
        public async Task<ActionResult<bool>> GetUserDiscountableStatus(Guid id)
        {
            int ordersNumber = (await mediator.Send(new GetOrdersByCustomerIdQuery() { Id = id })).Count();
            if (ordersNumber == 0)
            {
                return Ok(false);
            }

            if (ordersNumber % Constants.DiscountedOrder == Constants.DiscountedOrder - 1)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpGet("max-price")]
        public async Task<ActionResult<double>> GetMaxPriceOfOrder()
        {
            return await mediator.Send(new GetMaxPriceOfOrderQuery());
        }

        [RoleAuthorize("OPERATOR")]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest command)
        {
            if (command == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            User operatorUser = await mediator.Send(new GetUserByIdQuery() { Id = command.OperatorId });

            if (operatorUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, command.OperatorId));
            }

            User customerUser = await mediator.Send(new GetUserByIdQuery() { Id = command.UserId });

            if (customerUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, command.UserId));
            }

            if (operatorUser.Role != Role.OPERATOR || customerUser.Role != Role.CUSTOMER)
            {
                return BadRequest(Messages.InvalidData);
            }

            double totalPrice = 0;

            foreach (AddProductToOrderRequest request in command.Products)
            {
                Product product = await mediator.Send(new GetProductByIdQuery() { Id = request.ProductId });

                if (product != null)
                {
                    totalPrice += product.Price * request.Quantity;
                }
                else
                {
                    return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, request.ProductId));
                }
            }

            Guid orderId = await mediator.Send(new CreateOrderCommand() { OperatorId = command.OperatorId, UserId = command.UserId, Timestamp = command.Timestamp, Total = totalPrice });
            await CreateProductOrder(orderId, command);

            return CreatedAtAction("GetOrderById", new { id = orderId }, await mediator.Send(new GetOrderByIdQuery() { Id = orderId }));
        }

        [RoleAuthorize("ADMIN")]
        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportOrdersAsCsv([FromQuery] OrderFilterDto filter)
        {
            IEnumerable<Order> orders = await mediator.Send(new GetOrdersQuery());
            var queryable = _ordersService.Filter(orders.AsQueryable(), filter);
            string result = _ordersService.ConvertToCsv(queryable);
            return File(Encoding.UTF8.GetBytes(result), "text/csv", Constants.ReportFilename);
        }

        private async Task CreateProductOrder(Guid orderId, CreateOrderRequest request)
        {
            foreach (AddProductToOrderRequest addProductToOrderRequest in request.Products)
            {
                await mediator.Send(new CreateOrderProductCommand()
                {
                    OrderId = orderId,
                    ProductId = addProductToOrderRequest.ProductId,
                    Quantity = addProductToOrderRequest.Quantity
                });
            }
        }
    }
}

using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
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

namespace WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IRepository<OrderProduct> _orderProductRepository;
        private readonly IRepository<Product> _productsRepository;
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersRepository ordersRepository, IRepository<User> usersRepository,
                                IRepository<OrderProduct> orderPorductRepository, IRepository<Product> productsRepository,
                                IOrdersService ordersService)
        {
            _ordersRepository = ordersRepository;
            _usersRepository = usersRepository;
            _orderProductRepository = orderPorductRepository;
            _productsRepository = productsRepository;
            _ordersService = ordersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] PaginationDto pagination)
        {
            var queryable = _ordersRepository.GetAllAsQueryable();
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("customers/{customerId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByCustomerId(Guid customerId, [FromQuery] PaginationDto pagination)
        {
            User customerUser = await _usersRepository.GetByIdAsync(customerId);
            if (customerUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, customerId));
            }

            if (customerUser.Role != Role.CUSTOMER)
            {
                return BadRequest(Messages.InvalidData);
            }

            var queryable = _ordersRepository.GetAllAsQueryable().Where(order => order.UserId == customerId);
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("operators/{operatorId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByOperatorId(Guid operatorId, [FromQuery] PaginationDto pagination)
        {
            User operatorUser = await _usersRepository.GetByIdAsync(operatorId);
            if (operatorUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, operatorId));
            }

            if (operatorUser.Role != Role.OPERATOR)
            {
                return BadRequest(Messages.InvalidData);
            }

            var queryable = _ordersRepository.GetAllAsQueryable().Where(order => order.OperatorId == operatorId);
            await HttpContext.InsertPaginationParameterInResponse(queryable, pagination.EntitiesPerPage);
            return await queryable.Paginate(pagination).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            Order order = await _ordersRepository.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.OrderEntity, id));
            }

            return Ok(order);
        }

        [RoleAuthorize("OPERATOR")]
        [HttpPost]
        public async Task<ActionResult> CreateOrder(CreateOrderRequest orderRequest)
        {
            if (orderRequest == null)
            {
                return BadRequest(Messages.InvalidData);
            }

            User operatorUser = await _usersRepository.GetByIdAsync(orderRequest.OperatorId);
            if (operatorUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, orderRequest.OperatorId));
            }

            User customerUser = await _usersRepository.GetByIdAsync(orderRequest.UserId);
            if (customerUser == null)
            {
                return NotFound(Messages.NotFoundMessage(EntitiesConstants.UserEntity, orderRequest.UserId));
            }

            if (operatorUser.Role != Role.OPERATOR || customerUser.Role != Role.CUSTOMER)
            {
                return BadRequest(Messages.InvalidData);
            }

            double totalPrice = 0;
            foreach (AddProductToOrderRequest request in orderRequest.Products)
            {
                Product product = await _productsRepository.GetByIdAsync(request.ProductId);
                if (product != null)
                {
                    totalPrice += product.Price * request.Quantity;
                }
                else
                {
                    return NotFound(Messages.NotFoundMessage(EntitiesConstants.ProductEntity, request.ProductId));
                }
            }

            Order order = new() { OperatorId = orderRequest.OperatorId, UserId = orderRequest.UserId, Timestamp = orderRequest.Timestamp, Total = totalPrice };
            await _ordersRepository.CreateAsync(order);
            await CreateProductOrder(order.Id, orderRequest);

            return CreatedAtAction("GetOrderById", new { id = order.Id }, order);
        }

        [RoleAuthorize("ADMIN")]
        [HttpGet("export-csv")]
        public async Task<IActionResult> ExportOrdersAsCsv()
        {
            IEnumerable<Order> orders = await _ordersRepository.GetAllAsync();
            string result = _ordersService.ConvertToCsv(orders);
            return File(Encoding.UTF8.GetBytes(result), "text/csv", Constants.ReportFilename);
        }

        private async Task CreateProductOrder(Guid orderId, CreateOrderRequest orderRequest)
        {
            foreach (AddProductToOrderRequest request in orderRequest.Products)
            {
                await _orderProductRepository.CreateAsync(new OrderProduct()
                {
                    OrderId = orderId,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                });
            }
        }
    }
}

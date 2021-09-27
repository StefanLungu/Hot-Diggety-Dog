using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderProductFeatures.Commands
{
    public class CreateOrderProductCommandHandler : IRequestHandler<CreateOrderProductCommand, Guid>
    {
        private readonly IRepository<OrderProduct> _orderProductRepository;

        public CreateOrderProductCommandHandler(IRepository<OrderProduct> orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }

        public async Task<Guid> Handle(CreateOrderProductCommand request, CancellationToken cancellationToken)
        {
            OrderProduct orderProduct = new() 
            { 
                Order = request.Order, 
                Product = request.Product, 
                OrderId = request.OrderId, 
                ProductId = request.ProductId, 
                Quantity = request.Quantity 
            };

            await _orderProductRepository.CreateAsync(orderProduct);
            return orderProduct.Id;
        }
    }
}

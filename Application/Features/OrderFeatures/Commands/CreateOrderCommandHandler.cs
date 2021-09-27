using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrdersRepository _ordersRepository;

        public CreateOrderCommandHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new()
            {
                OperatorId = request.OperatorId,
                UserId = request.UserId,
                Timestamp = request.Timestamp,
                Total = request.Total
            };

            await _ordersRepository.CreateAsync(order);
            return order.Id;
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrdersByOperatorIdHandler : IRequestHandler<GetOrdersByOperatorIdQuery, IQueryable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetOrdersByOperatorIdHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<IQueryable<Order>> Handle(GetOrdersByOperatorIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_ordersRepository.GetAllAsQueryable().Where(order => order.OperatorId == request.Id));
        }
    }
}

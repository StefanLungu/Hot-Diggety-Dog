using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Qureries
{
    public class GetOrdersByCustomerIdHandler : IRequestHandler<GetOrdersByCustomerIdQuery, IQueryable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetOrdersByCustomerIdHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public Task<IQueryable<Order>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_ordersRepository.GetAllAsQueryable().Where(order => order.UserId == request.Id));
        }
    }
}

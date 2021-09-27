using Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetMaxPriceOfOrderQueryHandler : IRequestHandler<GetMaxPriceOfOrderQuery, double>
    {
        private readonly IOrdersRepository orderRepository;

        public GetMaxPriceOfOrderQueryHandler(IOrdersRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<double> Handle(GetMaxPriceOfOrderQuery request, CancellationToken cancellationToken)
        {
            return await orderRepository.GetMaxPriceOfOrdersAsync();
        }
    }
}

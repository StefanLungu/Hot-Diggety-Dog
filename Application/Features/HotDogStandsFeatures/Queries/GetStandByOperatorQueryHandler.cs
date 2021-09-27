using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandByOperatorQueryHandler : IRequestHandler<GetStandByOperatorQuery, HotDogStand>
    {
        private readonly IHotDogStandsRepository standRepository;

        public GetStandByOperatorQueryHandler(IHotDogStandsRepository standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<HotDogStand> Handle(GetStandByOperatorQuery request, CancellationToken cancellationToken)
        {
            return await standRepository.GetStandByOperatorIdAsync(request.OperatorId);
        }
    }
}

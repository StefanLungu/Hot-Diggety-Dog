using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandByIdQueryHandler : IRequestHandler<GetStandByIdQuery, HotDogStand>
    {
        private readonly IRepository<HotDogStand> standRepository;

        public GetStandByIdQueryHandler(IRepository<HotDogStand> standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<HotDogStand> Handle(GetStandByIdQuery request, CancellationToken cancellationToken)
        {
            return await standRepository.GetByIdAsync(request.Id);
        }
    }
}
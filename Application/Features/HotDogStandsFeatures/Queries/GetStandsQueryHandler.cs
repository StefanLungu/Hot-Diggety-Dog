using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandsQueryHandler : IRequestHandler<GetStandsQuery, IEnumerable<HotDogStand>>
    {
        private readonly IRepository<HotDogStand> standRepository;

        public GetStandsQueryHandler(IRepository<HotDogStand> standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<IEnumerable<HotDogStand>> Handle(GetStandsQuery request, CancellationToken cancellationToken)
        {
            return await standRepository.GetAllAsync();
        }
    }
}
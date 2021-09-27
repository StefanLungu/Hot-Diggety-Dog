using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    public class CreateStandCommandHandler : IRequestHandler<CreateStandCommand, Guid>
    {
        private readonly IRepository<HotDogStand> standRepository;

        public CreateStandCommandHandler(IRepository<HotDogStand> standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<Guid> Handle(CreateStandCommand request, CancellationToken cancellationToken)
        {
            HotDogStand stand = new()
            {
                Address = request.Address,
                OperatorId = request.OperatorId
            };

            await standRepository.CreateAsync(stand);
            return stand.Id;
        }
    }
}
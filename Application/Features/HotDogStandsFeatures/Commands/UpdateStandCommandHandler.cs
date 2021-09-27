using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    class UpdateStandCommandHandler : IRequestHandler<UpdateStandCommand, Guid>
    {
        private readonly IRepository<HotDogStand> standRepository;

        public UpdateStandCommandHandler(IRepository<HotDogStand> standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<Guid> Handle(UpdateStandCommand request, CancellationToken cancellationToken)
        {
            HotDogStand stand = await standRepository.GetByIdAsync(request.Id);

            if (stand == null)
            {
                return Guid.Empty;
            }

            stand.Address = request.Address;
            stand.OperatorId = request.OperatorId;

            await standRepository.UpdateAsync(stand);
            return stand.Id;
        }
    }
}
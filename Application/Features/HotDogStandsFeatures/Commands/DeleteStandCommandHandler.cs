using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    class DeleteStandCommandHandler : IRequestHandler<DeleteStandCommand, Guid>
    {
        private readonly IRepository<HotDogStand> standRepository;

        public DeleteStandCommandHandler(IRepository<HotDogStand> standRepository)
        {
            this.standRepository = standRepository;
        }

        public async Task<Guid> Handle(DeleteStandCommand request, CancellationToken cancellationToken)
        {
            HotDogStand stand = await standRepository.GetByIdAsync(request.Id);

            if (stand == null)
            {
                return Guid.Empty;
            }

            await standRepository.RemoveAsync(stand);
            return stand.Id;
        }
    }
}
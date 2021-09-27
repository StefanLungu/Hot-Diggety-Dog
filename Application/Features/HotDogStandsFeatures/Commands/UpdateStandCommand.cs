using MediatR;
using System;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    public class UpdateStandCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid OperatorId { get; set; }
        public string Address { get; set; }
    }
}

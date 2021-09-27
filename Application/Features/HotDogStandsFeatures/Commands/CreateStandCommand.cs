using MediatR;
using System;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    public class CreateStandCommand : IRequest<Guid>
    {
        public string Address { get; set; }
        public Guid OperatorId { get; set; }
    }
}
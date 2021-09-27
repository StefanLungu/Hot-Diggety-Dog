using MediatR;
using System;

namespace Application.Features.HotDogStandsFeatures.Commands
{
    public class DeleteStandCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
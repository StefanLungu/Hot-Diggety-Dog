using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandByOperatorQuery : IRequest<HotDogStand>
    {
        public Guid OperatorId { get; set; }
    }
}

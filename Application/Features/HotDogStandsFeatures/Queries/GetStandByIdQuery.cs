using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandByIdQuery : IRequest<HotDogStand>
    {
        public Guid Id { get; set; }
    }
}
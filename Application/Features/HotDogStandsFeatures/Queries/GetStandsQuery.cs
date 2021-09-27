using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.HotDogStandsFeatures.Queries
{
    public class GetStandsQuery : IRequest<IEnumerable<HotDogStand>>
    {
    }
}
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.UserFeatures.Queries
{
    public class GetCustomersQuery : IRequest<IEnumerable<User>>
    {
    }
}

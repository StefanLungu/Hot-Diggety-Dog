using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.UserFeatures.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>
    {
    }
}

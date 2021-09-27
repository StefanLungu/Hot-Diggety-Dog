using Domain.Entities;
using MediatR;

namespace Application.Features.UserFeatures.Queries
{
    public class GetUserByUsernameAndPasswordQuery : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

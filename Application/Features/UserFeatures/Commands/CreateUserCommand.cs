using Domain.Entities;
using MediatR;

namespace Application.Features.UserFeatures.Commands
{
    public class CreateUserCommand : IRequest<User>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}

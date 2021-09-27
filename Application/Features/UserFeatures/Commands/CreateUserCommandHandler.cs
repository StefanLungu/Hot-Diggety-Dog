using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Application.Features.UserFeatures.Commands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUsersRepository usersRepository;

        public CreateUserCommandHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository; 
        }

        async Task<User> IRequestHandler<CreateUserCommand, User>.Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await usersRepository.ExistsByEmailAsync(request.Email) || await usersRepository.ExistsByUsernameAsync(request.Username))
            {
                return null;
            }

            User user = new()
            {
                Username = request.Username,
                Email = request.Email,
                Password = Crypto.SHA256(request.Password),
                Role = Role.CUSTOMER
            };

            await usersRepository.CreateAsync(user);
            return user;
        }
    }
}

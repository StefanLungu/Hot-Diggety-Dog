using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, User>
    {
        private readonly IUsersRepository usersRepository;

        public AuthenticateUserQueryHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<User> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
        {
            return await usersRepository.GetByUsernameAndPassword(request.Username, request.Password);
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    class GetUserByUsernameAndPasswordQueryHandler : IRequestHandler<GetUserByUsernameAndPasswordQuery, User>
    {

        private readonly IUsersRepository usersRepository;

        public GetUserByUsernameAndPasswordQueryHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<User> Handle(GetUserByUsernameAndPasswordQuery request, CancellationToken cancellationToken)
        {
            return await usersRepository.GetByUsernameAndPassword(request.Username, request.Password);
        }
    }
}

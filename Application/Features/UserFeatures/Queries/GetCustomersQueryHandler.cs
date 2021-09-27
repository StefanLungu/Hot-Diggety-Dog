using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, IEnumerable<User>>
    {
        private readonly IUsersRepository usersRepository;

        public GetCustomersQueryHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await usersRepository.GetAllByRoleAsync(Role.CUSTOMER);
        }
    }
}

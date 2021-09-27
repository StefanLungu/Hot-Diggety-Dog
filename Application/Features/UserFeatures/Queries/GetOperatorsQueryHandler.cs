using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries
{
    public class GetOperatorsQueryHandler : IRequestHandler<GetOperatorsQuery, IEnumerable<User>>
    {
        private readonly IUsersRepository usersRepository;

        public GetOperatorsQueryHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<User>> Handle(GetOperatorsQuery request, CancellationToken cancellationToken)
        {
            return await usersRepository.GetAllByRoleAsync(Role.OPERATOR);
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands
{
    class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Guid>
    {
        private readonly IUsersRepository usersRepository;

        public DeleteUserCommandHandler(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await usersRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                return Guid.Empty;
            }

            await usersRepository.RemoveAsync(user);

            return user.Id;
        }
    }
}

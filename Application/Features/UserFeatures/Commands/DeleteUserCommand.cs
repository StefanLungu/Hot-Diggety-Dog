using MediatR;
using System;

namespace Application.Features.UserFeatures.Commands
{
    public class DeleteUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}

using MediatR;
using System;

namespace Application.Features.ProductFeatures.Commands
{
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}

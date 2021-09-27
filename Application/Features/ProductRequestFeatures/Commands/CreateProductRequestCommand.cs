using MediatR;
using System;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductRequestCommand : IRequest<Guid>
    {
        public Guid RequestId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}

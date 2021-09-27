using MediatR;
using System;

namespace Application.Features.ProductsRequestFeatures.Commands
{
    public class DeleteProductsRequestCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}

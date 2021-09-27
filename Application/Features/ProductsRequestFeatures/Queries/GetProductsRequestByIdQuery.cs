using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.ProductsRequestFeatures.Queries
{
    public class GetProductsRequestByIdQuery : IRequest<ProductsRequest>
    {
        public Guid Id { get; set; }
    }
}

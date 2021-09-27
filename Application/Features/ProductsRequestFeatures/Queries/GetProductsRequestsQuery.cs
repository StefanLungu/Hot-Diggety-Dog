using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ProductsRequestFeatures.Queries
{
    public class GetProductsRequestsQuery : IRequest<IEnumerable<ProductsRequest>>
    {
    }
}

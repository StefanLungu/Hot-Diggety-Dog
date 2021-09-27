using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.ProductFeatures.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}

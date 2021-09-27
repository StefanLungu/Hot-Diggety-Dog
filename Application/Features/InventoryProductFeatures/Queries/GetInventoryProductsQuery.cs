using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.InventoryProductFeatures.Queries
{
    public class GetInventoryProductsQuery : IRequest<IEnumerable<InventoryProduct>>
    {
    }
}

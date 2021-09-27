using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.InventoryProductFeatures.Queries
{
    public class GetInventoryProductsQueryHandler : IRequestHandler<GetInventoryProductsQuery, IEnumerable<InventoryProduct>>
    {
        private readonly IInventoryProductsRepository inventoryProductsRepository;

        public GetInventoryProductsQueryHandler(IInventoryProductsRepository inventoryProductsRepository)
        {
            this.inventoryProductsRepository = inventoryProductsRepository;
        }

        public async Task<IEnumerable<InventoryProduct>> Handle(GetInventoryProductsQuery request, CancellationToken cancellationToken)
        {
            return await inventoryProductsRepository.GetAllAsync();
        }
    }
}

using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.InventoryProductFeatures.Commands
{
    public class UpdateInventoryProductCommandHandler : IRequestHandler<UpdateInventoryProductCommand, Guid>
    {
        private readonly IInventoryProductsRepository inventoryProductsRepository;

        public UpdateInventoryProductCommandHandler(IInventoryProductsRepository inventoryProductsRepository)
        {
            this.inventoryProductsRepository = inventoryProductsRepository;
        }

        public async Task<Guid> Handle(UpdateInventoryProductCommand request, CancellationToken cancellationToken)
        {
            InventoryProduct inventoryProduct = await inventoryProductsRepository.GetInventoryProductByProductIdAsync(request.ProductId);

            if (inventoryProduct == null)
            {
                return Guid.Empty;
            }

            inventoryProduct.Quantity = request.Quantity;
            await inventoryProductsRepository.UpdateAsync(inventoryProduct);
            return inventoryProduct.Id;
        }
    }
}

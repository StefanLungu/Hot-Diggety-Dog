using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IRepository<Product> productRepository;
        private readonly IInventoryProductsRepository inventoryProductsRepository;

        public DeleteProductCommandHandler(IRepository<Product> productRepository, IInventoryProductsRepository inventoryProductsRepository)
        {
            this.productRepository = productRepository;
            this.inventoryProductsRepository = inventoryProductsRepository;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return Guid.Empty;
            }

            await productRepository.RemoveAsync(product);

            InventoryProduct inventoryProduct = await inventoryProductsRepository.GetInventoryProductByProductIdAsync(request.Id);
            await inventoryProductsRepository.RemoveAsync(inventoryProduct);

            return product.Id;
        }
    }
}

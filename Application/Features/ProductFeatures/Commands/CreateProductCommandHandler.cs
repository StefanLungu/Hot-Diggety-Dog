using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IRepository<Product> productRepository;
        private readonly IInventoryProductsRepository inventoryProductsRepository;

        public CreateProductCommandHandler(IRepository<Product> productRepository, IInventoryProductsRepository inventoryProductsRepository)
        {
            this.productRepository = productRepository;
            this.inventoryProductsRepository = inventoryProductsRepository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new()
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description,
                Category = request.Category
            };
            await productRepository.CreateAsync(product);

            InventoryProduct inventoryProduct = new()
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                Product = product,
                Quantity = 0
            };
            await inventoryProductsRepository.CreateAsync(inventoryProduct);

            return product.Id;
        }
    }
}

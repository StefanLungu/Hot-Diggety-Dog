using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands
{
    class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IRepository<Product> productRepository;

        public UpdateProductCommandHandler(IRepository<Product> productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                return Guid.Empty;
            }

            product.Name = request.Name;
            product.Price = request.Price;
            product.Category = request.Category;
            product.Description = request.Description;

            await productRepository.UpdateAsync(product);
            return product.Id;
        }
    }
}

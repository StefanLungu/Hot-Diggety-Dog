using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductsRequestFeatures.Commands
{
    class DeleteProductsRequestCommandHandler : IRequestHandler<DeleteProductsRequestCommand, Guid>
    {
        private readonly IRepository<ProductRequest> productRequestsRepository;
        private readonly IProductsRequestsRepository requestsRepository;

        public DeleteProductsRequestCommandHandler(IProductsRequestsRepository requestsRepository, IRepository<ProductRequest> productRequestsRepository)
        {
            this.productRequestsRepository = productRequestsRepository;
            this.requestsRepository = requestsRepository;
        }

        public async Task<Guid> Handle(DeleteProductsRequestCommand request, CancellationToken cancellationToken)
        {
            ProductsRequest productsRequest = await requestsRepository.GetByIdAsync(request.Id);
            if (productsRequest == null)
            {
                return Guid.Empty;
            }

            foreach (var item in productsRequest.ProductRequests)
            {
                await productRequestsRepository.RemoveAsync(item);
            }

            await requestsRepository.RemoveAsync(productsRequest);

            return productsRequest.Id;
        }
    }
}

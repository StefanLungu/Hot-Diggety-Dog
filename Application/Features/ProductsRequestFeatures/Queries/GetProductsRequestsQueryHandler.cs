using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.ProductsRequestFeatures.Queries
{
    public class GetProductsRequestsQueryHandler : IRequestHandler<GetProductsRequestsQuery, IEnumerable<ProductsRequest>>
    {
        private readonly IRepository<ProductsRequest> productsRequestRepository;

        public GetProductsRequestsQueryHandler(IRepository<ProductsRequest> productsRequestRepository)
        {
            this.productsRequestRepository = productsRequestRepository;
        }

        public async Task<IEnumerable<ProductsRequest>> Handle(GetProductsRequestsQuery request, CancellationToken cancellationToken)
        {
            return await productsRequestRepository.GetAllAsync();
        }
    }
}

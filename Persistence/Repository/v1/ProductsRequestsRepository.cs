using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading.Tasks;

namespace Persistence.Repository.v1
{
    public class ProductsRequestsRepository : Repository<ProductsRequest>, IProductsRequestsRepository
    {
        public ProductsRequestsRepository(DataContext context) : base(context)
        {

        }

        public override async Task<ProductsRequest> GetByIdAsync(Guid id)
        {
            return await _context.ProductsRequests.Include(request => request.ProductRequests)
                                  .ThenInclude(productRequest => productRequest.Product)
                                  .FirstOrDefaultAsync(request => request.Id == id);
        }
    }
}

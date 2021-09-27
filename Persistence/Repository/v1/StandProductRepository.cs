using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository.v1
{
    public class StandProductRepository : Repository<HotDogStandProduct>, IStandProductRepository
    {
        public StandProductRepository(DataContext context) : base(context)
        {

        }

        public async Task<HotDogStandProduct> GetStandProductByProductId(Guid standId, Guid productId)
        {
            return await _context.HotDogStandProducts
                .Where(standProduct => standProduct.StandId == standId && standProduct.ProductId == productId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HotDogStandProduct>> GetStandProducts(Guid standId)
        {
            return await _context.HotDogStandProducts
                .Where(standProduct => standProduct.StandId == standId)
                .ToListAsync();
        }
    }
}

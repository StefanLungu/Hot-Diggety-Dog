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
    public class InventoryProductsRepository : Repository<InventoryProduct>, IInventoryProductsRepository
    {
        public InventoryProductsRepository(DataContext context) : base(context)
        {

        }

        public async Task<InventoryProduct> GetInventoryProductByProductIdAsync(Guid productId)
        {
            return await _context.InventoryProducts
                .Where(inventoryProduct => inventoryProduct.ProductId.Equals(productId))
                .FirstOrDefaultAsync();
        }

        public override async Task<IEnumerable<InventoryProduct>> GetAllAsync()
        {
            return await _context.InventoryProducts
                .Include(inventoryProduct => inventoryProduct.Product)
                .ToListAsync();
        }
    }
}

using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IInventoryProductsRepository : IRepository<InventoryProduct>
    {
        Task<InventoryProduct> GetInventoryProductByProductIdAsync(Guid productId);
    }
}

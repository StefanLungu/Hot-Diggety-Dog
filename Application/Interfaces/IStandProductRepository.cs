using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStandProductRepository : IRepository<HotDogStandProduct>
    {
        Task<IEnumerable<HotDogStandProduct>> GetStandProducts(Guid standId);
        Task<HotDogStandProduct> GetStandProductByProductId(Guid standId, Guid productId);
    }
}

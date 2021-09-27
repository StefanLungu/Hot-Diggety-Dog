using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHotDogStandsRepository : IRepository<HotDogStand>
    {
        Task<HotDogStand> GetStandByOperatorIdAsync(Guid id);
    }
}

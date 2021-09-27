using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Threading.Tasks;

namespace Persistence.Repository.v1
{
    public class HotDogStandsRepository : Repository<HotDogStand>, IHotDogStandsRepository
    {
        public HotDogStandsRepository(DataContext context) : base(context)
        {

        }

        public async Task<HotDogStand> GetStandByOperatorIdAsync(Guid id)
        {
            return await _context.HotDogStands
                .Include(stand => stand.StandProducts)
                .ThenInclude(standProduct => standProduct.Product)
                .FirstOrDefaultAsync(stand => stand.OperatorId == id);
        }
    }
}

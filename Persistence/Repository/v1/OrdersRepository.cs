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
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(DataContext context) : base(context)
        {

        }

        public IQueryable<Order> GetAllAsQueryable()
        {
            return _context.Orders.AsQueryable();
        }

        public override async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(order => order.OrderProducts)
                                  .ThenInclude(orderProduct => orderProduct.Product)
                                  .FirstOrDefaultAsync(order => order.Id == id);
        }

        public async Task<double> GetMaxPriceOfOrdersAsync()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            if (orders.Count == 0)
            {
                return 0.0;
            }
            return orders.Select(order => order.Total).Max();
        }
    }
}

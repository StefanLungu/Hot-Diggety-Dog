using Domain.Dtos;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface IOrdersService
    {
        string ConvertToCsv(IEnumerable<Order> orders);
        IQueryable<Order> Filter(IQueryable<Order> orders, OrderFilterDto filter);
    }
}

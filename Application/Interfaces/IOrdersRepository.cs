using Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrdersRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllAsQueryable();
        Task<double> GetMaxPriceOfOrdersAsync();
    }
}

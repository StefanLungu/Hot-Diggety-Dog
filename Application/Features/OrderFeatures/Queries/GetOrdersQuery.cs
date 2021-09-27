using Domain.Entities;
using MediatR;
using System.Linq;

namespace Application.Features.OrderFeatures.Qureries
{
    public class GetOrdersQuery: IRequest<IQueryable<Order>>
    {
    }
}

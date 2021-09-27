using Domain.Entities;
using MediatR;
using System;
using System.Linq;

namespace Application.Features.OrderFeatures.Queries
{
    public class GetOrdersByOperatorIdQuery : IRequest<IQueryable<Order>>
    {
        public Guid Id { get; set; }
    }
}

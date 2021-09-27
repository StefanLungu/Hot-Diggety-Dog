using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.OrderFeatures.Qureries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }
    }
}

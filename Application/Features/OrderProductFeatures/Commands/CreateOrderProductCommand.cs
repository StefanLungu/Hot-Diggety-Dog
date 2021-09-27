using Domain.Entities;
using MediatR;
using System;

namespace Application.Features.OrderProductFeatures.Commands
{
    public class CreateOrderProductCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }

        public virtual Order Order { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

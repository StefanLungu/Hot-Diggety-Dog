using MediatR;
using System;

namespace Application.Features.InventoryProductFeatures.Commands
{
    public class UpdateInventoryProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public uint Quantity { get; set; }
    }
}

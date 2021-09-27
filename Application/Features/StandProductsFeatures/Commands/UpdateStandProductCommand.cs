using MediatR;
using System;

namespace Application.Features.StandProductsFeatures.Commands
{
    public class UpdateStandProductCommand : IRequest<Guid>
    {
        public Guid StandId { set; get; }
        public Guid ProductId { set; get; }
        public int NewQuantity { set; get; }
    }
}

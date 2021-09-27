using MediatR;
using System;

namespace Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { set; get; }
        public string Category { get; set; }
    }
}

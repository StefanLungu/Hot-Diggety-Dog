using MediatR;
using System;

namespace Application.Features.ProductFeatures.Commands
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public string Description { set; get; }
        public string Category { get; set; }
    }
}

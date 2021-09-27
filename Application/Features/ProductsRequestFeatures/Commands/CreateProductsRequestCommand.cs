using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.ProductsRequestFeatures.Commands
{
    public class CreateProductsRequestCommand : IRequest<Guid>
    {
        [Required]
        public Guid OperatorId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

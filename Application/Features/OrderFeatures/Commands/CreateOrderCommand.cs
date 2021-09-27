using Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.OrderFeatures.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        [Required]
        public Guid OperatorId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public double Total { get; set; }

        [Required]
        public List<AddProductToOrderRequest> Products { get; set; } = new List<AddProductToOrderRequest>();
    }
}

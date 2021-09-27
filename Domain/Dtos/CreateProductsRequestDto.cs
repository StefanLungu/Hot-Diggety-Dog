using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class CreateProductsRequestDto
    {
        [Required]
        public Guid OperatorId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public List<CreateProductRequestDto> Products { get; set; }
    }
}

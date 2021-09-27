using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class CreateProductRequestDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}

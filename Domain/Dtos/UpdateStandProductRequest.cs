using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos
{
    public class UpdateStandProductsRequest
    {
        [Required]
        public Guid StandId { set; get; }

        [Required]
        public List<AddProductToStandRequest> Products { set; get; }
    }
}

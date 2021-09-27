using Domain.Common;
using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class ProductRequest : BaseEntity
    {
        public Guid ProductsRequestId { get; set; }

        [JsonIgnore]
        public virtual ProductsRequest ProductsRequest { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

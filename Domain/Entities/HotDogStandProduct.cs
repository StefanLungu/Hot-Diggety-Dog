using Domain.Common;
using System;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class HotDogStandProduct : BaseEntity
    {
        public Guid StandId { get; set; }

        [JsonIgnore]
        public virtual HotDogStand Stand { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

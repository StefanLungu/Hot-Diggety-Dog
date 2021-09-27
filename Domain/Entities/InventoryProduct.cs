using Domain.Common;
using System;

namespace Domain.Entities
{
    public class InventoryProduct : BaseEntity
    {
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public uint Quantity { get; set; }
    }
}

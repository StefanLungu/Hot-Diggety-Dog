using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class HotDogStand : BaseEntity
    {
        public string Address { get; set; }
        public Guid OperatorId { set; get; }
        public virtual User Operator { get; set; }
        public virtual ICollection<HotDogStandProduct> StandProducts { get; set; }
    }
}

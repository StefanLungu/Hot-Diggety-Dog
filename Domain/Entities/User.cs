using Domain.Common;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        [JsonIgnore]
        public virtual HotDogStand Stand { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<Order> ClientOrders { get; set; }
        [JsonIgnore]
        public ICollection<Order> OperatorOrders { get; set; }
        [JsonIgnore]
        public ICollection<ProductsRequest> OperatorRequests { get; set; }
    }
}

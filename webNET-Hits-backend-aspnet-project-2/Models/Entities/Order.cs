using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class Order: BaseEntity
    {
        public Guid UserId { get; set; }
        [ValidDeliveryTime]
        public DateTime DeliveryTime { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.InProcess; 
        public decimal Price { get; set; }
        [MinLength(1)]
        public string Address { get; set; }
        public IEnumerable<DishInBasket> Dishes { get; set; }
    }
}

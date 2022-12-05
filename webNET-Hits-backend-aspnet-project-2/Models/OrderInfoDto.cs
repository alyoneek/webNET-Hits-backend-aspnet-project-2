using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class OrderInfoDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public Decimal Price { get; set; }
    }
}

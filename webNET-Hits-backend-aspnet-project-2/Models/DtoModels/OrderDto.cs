using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DeliveryTime { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MinLength(1)]
        public string Address { get; set; }
        public IEnumerable<DishBasketDto> Dishes { get; set; }
    }
}

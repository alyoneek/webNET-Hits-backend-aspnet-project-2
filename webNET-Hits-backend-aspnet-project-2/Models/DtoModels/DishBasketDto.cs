using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class DishBasketDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public int Amount { get; set; }
        [Url]
        public string? Image { get; set; }
    }
}

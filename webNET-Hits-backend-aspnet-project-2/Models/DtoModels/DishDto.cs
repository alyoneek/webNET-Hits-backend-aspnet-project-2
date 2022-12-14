using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class DishDto
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public bool Vegetarian { get; set; }
        public double? Rating { get; set; }
        [Required]
        public DishCategoryType Category { get; set; }
    }
}

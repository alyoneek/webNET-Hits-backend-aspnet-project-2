using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class DishDto
    {
        public Guid Id { get; set; }
        [MinLength(1)]
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public Boolean Vegetarian { get; set; }
        public double? Rating { get; set; }
        public DishCategoryType Category { get; set; }
    }
}

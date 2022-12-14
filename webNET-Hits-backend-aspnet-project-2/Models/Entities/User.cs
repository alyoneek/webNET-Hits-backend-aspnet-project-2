using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class User: BaseEntity
    {
        [MinLength(1)]
        public string FullName { get; set; }
        [MinLength(1)]
        [EmailAddress]
        [RegularExpression(@"^[\w\\.-]+@[a-z]+\.[a-z]+$")]
        public string Email { get; set; }
        [ValidPassword(6, 20)]
        public string Password { get; set; }
        public string? Address { get; set; }
        [ValidDate]
        public DateTime? BirthDate { get; set; }
        [ValidEnum]
        public GenderType Gender { get; set; }
        [Phone]
        [RegularExpression(@"^\+7\s*\(?\d{3}\)?\s*\d{3}\s*-?\s*\d{2}\s*-?\s*\d{2}$")]
        public string? PhoneNumber { get; set; }

        public IEnumerable<DishInBasket> DishesInBasket { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.Entities
{
    public class User: BaseEntity
    {
        [MinLength(1)]
        public string FullName { get; set; }
        [MinLength(1)]
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(6)]
        public string Password { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }

        public IEnumerable<DishInBasket> DishesInBasket { get; set; }
    }
}

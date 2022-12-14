using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DtoModels
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я-\s]+$")]
        public string FullName { get; set; }
        [ValidDate]
        public DateTime? BirthDate { get; set; }
        [Required]
        [ValidEnum]
        public GenderType Gender { get; set; }
        public string? Address { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w\\.-]+@[a-z]+\.[a-z]+$")]
        public string? Email { get; set; }
        [Phone]
        [RegularExpression(@"^\+7\s*\(?\d{3}\)?\s*\d{3}\s*-?\s*\d{2}\s*-?\s*\d{2}$")]
        public string? PhoneNumber { get; set; }
    }
}

using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class UserRegisterModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я-\s]+$",
            ErrorMessage = "Only letters are allowed")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[\w\\.-]+@[a-z]+\.[a-z]+$", 
            ErrorMessage = "The Email field is not a valid e-mail address")]
        public string Email { get; set; }
        [Required]
        [ValidPassword(6, 20)]
        public string Password { get; set; }
        public string? Address { get; set; }
        [ValidDate]
        public DateTime? BirthDate { get; set; }
        [Required]
        [ValidEnum(ErrorMessage = "The field Gender is required.")]
        public GenderType Gender { get; set; }
        [Phone]
        [RegularExpression(@"^\+7\s*\(?\d{3}\)?\s*\d{3}\s*-?\s*\d{2}\s*-?\s*\d{2}$",
            ErrorMessage = "The Phone field must match +7 (xxx) xxx-xx-xx example or +7xxxxxxxxxx examples")]
        public string? PhoneNumber { get; set; }
    }
}

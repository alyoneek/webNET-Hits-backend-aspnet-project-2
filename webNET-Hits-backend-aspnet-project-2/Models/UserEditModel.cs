using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class UserEditModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Zа-яА-Я-\s]+$",
            ErrorMessage = "Only letters are allowed")]
        public string FullName { get; set; }
        public string? Address { get; set; }
        [ValidDate]
        public DateTime? BirthDate { get; set; }
        [Required]
        [ValidEnum(ErrorMessage="The field Gender is required.")]
        public GenderType? Gender { get; set; }
        [Phone]
        [RegularExpression(@"^\+7\s*\(?\d{3}\)?\s*\d{3}\s*-?\s*\d{2}\s*-?\s*\d{2}$",
            ErrorMessage = "The Phone field must match +7 (xxx) xxx-xx-xx or +7xxxxxxxxxx examples")]
        public string? PhoneNumber { get; set; }
    }
}

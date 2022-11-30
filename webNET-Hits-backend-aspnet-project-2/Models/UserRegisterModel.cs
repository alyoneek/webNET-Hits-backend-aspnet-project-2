using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class UserRegisterModel
    {
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        [Required]
        [MinLength(1)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public GenderType Gender { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}

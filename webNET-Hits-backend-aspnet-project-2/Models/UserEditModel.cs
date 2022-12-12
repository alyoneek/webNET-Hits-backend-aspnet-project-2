using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Attributes;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class UserEditModel
    {
        [Required]
        [MinLength(1)]
        public string FullName { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        [ValidEnum(ErrorMessage="The field Gender is required.")]
        public GenderType? Gender { get; set; }
        [Phone]
        public string? PhoneNumber { get; set; }
    }
}

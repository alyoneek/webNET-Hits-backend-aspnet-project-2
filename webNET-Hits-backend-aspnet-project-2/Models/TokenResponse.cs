using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class TokenResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderType Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public string Token { get; set; }

        public TokenResponse(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Password = user.Password;
            Address = user.Address;
            BirthDate = user.BirthDate;
            Gender = user.Gender;
            PhoneNumber = user.PhoneNumber;
            Token = token;
        }
    }
}

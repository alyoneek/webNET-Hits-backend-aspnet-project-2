using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;
using webNET_Hits_backend_aspnet_project_2.Models.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public TokenResponse(string token)
        {
            Token = token;
        }
    }
}

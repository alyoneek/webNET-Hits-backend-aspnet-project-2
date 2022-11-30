using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public class JwtConfigurations
    {
        public const string Issuer = "JwtIssuer";
        public const string Audience = "JwtClient";
        private const string Key = "secretkeyclient0";
        public const int Lifetime = 60;
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

    }
}

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public class JwtConfigurations
    {
        public string Issuer {get; set; }
        public string Audience {get; set; }
        public string Secret { get; set; }
        public int Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }

    }
}

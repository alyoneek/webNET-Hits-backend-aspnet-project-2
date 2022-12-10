using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using webNET_Hits_backend_aspnet_project_2.Models.Entities;

namespace webNET_Hits_backend_aspnet_project_2.Helpers
{
    public interface IJwtUtils
    {
        public string GenerateToken(User user);
        public Guid? ValidateToken(string token);
    }
    public class JwtUtils : IJwtUtils
    {
        public readonly IOptions<JwtConfigurations> _authOptions;
        public JwtUtils(IOptions<JwtConfigurations> authOptions)
        {
            _authOptions = authOptions;
        }

        public string GenerateToken(User user)
        {
            var authParams = _authOptions.Value;
            var jwt = new JwtSecurityToken(
                issuer: authParams.Issuer,
                audience: authParams.Audience,
                notBefore: DateTime.UtcNow,
                claims: new[] { new Claim("id", user.Id.ToString()) },
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(authParams.Lifetime)),
                signingCredentials: new SigningCredentials(authParams.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public Guid? ValidateToken(string token)
        {
            if (token == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var authParams = _authOptions.Value;
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authParams.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authParams.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authParams.GetSymmetricSecurityKey(),
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return userId;
            }
            catch
            {
                return null;
            }
        }
    }
}

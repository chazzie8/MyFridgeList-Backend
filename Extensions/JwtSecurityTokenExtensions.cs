using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace MyFridgeListWebapi.Extensions
{
    public static class JwtSecurityTokenExtensions
    {
        public static Guid UserId(this JwtSecurityToken securityToken)
        {
            var claim = securityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            return Guid.Parse(claim?.Value);
        }

        public static string Token(this JwtSecurityToken securityToken)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}

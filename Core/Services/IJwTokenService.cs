using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MyFridgeListWebapi.Core.Data.Entities;

namespace MyFridgeListWebapi.Core.Services
{
    public interface IJwTokenService
    {
        JwtSecurityToken CreateToken(User user, IEnumerable<Claim> additionalClaims = null);
        JwtSecurityToken DeserializeToken(string token);
    }
}

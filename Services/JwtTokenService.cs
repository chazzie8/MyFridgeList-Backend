using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyFridgeListWebapi.Core.Data.Entities;
using MyFridgeListWebapi.Core.Environment;
using MyFridgeListWebapi.Core.Services;

namespace MyFridgeListWebapi.Services
{
    public sealed class JwtTokenService : IJwTokenService
    {
        private readonly JwtConfiguration _options;
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;

        public JwtTokenService(AppConfiguration appConfiguration)
        {
            _options = appConfiguration.JwtConfiguration;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        }

        public JwtSecurityToken CreateToken(User user, IEnumerable<Claim> additionalClaims = null)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Iss, _options.Issuer),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            var validFrom = DateTime.UtcNow;
            var expires = validFrom.AddSeconds(_options.LifetimeInSeconds);
            var securityToken = new JwtSecurityToken(
                _options.Issuer,
                _options.Issuer,
                claims,
                notBefore: validFrom,
                expires: expires,
                signingCredentials: _signingCredentials);

            return securityToken;
        }

        public JwtSecurityToken DeserializeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadJwtToken(token.Replace(JwtBearerDefaults.AuthenticationScheme, string.Empty).Trim());

            return securityToken;
        }
    }
}

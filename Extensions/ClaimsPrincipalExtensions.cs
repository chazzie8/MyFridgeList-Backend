using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyFridgeListWebapi.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            if (!principal.Identity.IsAuthenticated)
            {
                return Guid.Empty;
            }

            if (!principal.HasClaim(x => x.Type == JwtRegisteredClaimNames.Sub))
            {
                throw new Exception(string.Format("Principal is missing a claim of the type `{0}`.", JwtRegisteredClaimNames.Sub));
            }

            var userIdString = principal.FindFirst(JwtRegisteredClaimNames.Sub).Value;

            if (!Guid.TryParse(userIdString, out Guid result))
            {
                throw new Exception(string.Format("Principals `{0}`-Claim could not be Parsed to GUID, Value was: `{1}`.", JwtRegisteredClaimNames.Sub, userIdString));
            }

            return result;
        }
    }
}

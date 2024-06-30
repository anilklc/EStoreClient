using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace EStore.Services.Services
{
    public class TokenService
    {
        public static string[] GetRolesFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jsonToken != null)
            {
                var claims = jsonToken.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();
                return claims;
            }

            return new string[0];
        }
    }
}

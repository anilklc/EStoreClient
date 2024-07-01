using EStore.Services.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

public class TokenMiddleware
{
    private readonly RequestDelegate _next;

    public TokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["AccessToken"];

        if (!string.IsNullOrEmpty(token))
        {
            var roles = TokenService.GetRolesFromToken(token);
            
            var claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, "cookie");
            context.User = new ClaimsPrincipal(identity);

            var username = TokenService.GetUserNameFromToken(token);
            if (!string.IsNullOrEmpty(username))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, username));
            }
        }

        await _next(context);
    }
}

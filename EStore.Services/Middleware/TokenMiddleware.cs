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
        var token = context.Request.Cookies["AccessToken"]; // Cookie'den token'ı al

        if (!string.IsNullOrEmpty(token))
        {
            var roles = TokenService.GetRolesFromToken(token); // Token'den rolleri çöz

            var claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, "cookie");
            context.User = new ClaimsPrincipal(identity);
        }

        await _next(context);
    }
}

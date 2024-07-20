using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Helper
{
    public class UserHelper
    {
        public static bool IsUserLoggedIn(HttpContext httpContext)
        {
            var accessToken = httpContext.Request.Cookies["AccessToken"];
            return !string.IsNullOrEmpty(accessToken);
        }

        public static string GetUserName(HttpContext httpContext)
        {
  
            var accessToken = httpContext.Request.Cookies["AccessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                var userNameClaim = httpContext.User.FindFirst(ClaimTypes.Name);
                return userNameClaim?.Value;
            }
            return null;
        }

        public static string GetUserRole(HttpContext httpContext)
        {

            var accessToken = httpContext.Request.Cookies["AccessToken"];
            if (!string.IsNullOrEmpty(accessToken))
            {
                var userRoleClaim = httpContext.User.FindFirst(ClaimTypes.Role);
                return userRoleClaim?.Value;
            }
            return null;
        }
    }
}

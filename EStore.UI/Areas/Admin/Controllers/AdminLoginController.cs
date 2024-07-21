using EStore.Dto.Login;
using EStore.Services;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminLogin")]
    public class AdminLoginController : BaseController
    {
        private readonly ILoginService<LoginResponse> _loginService;

        public AdminLoginController(ILoginService<LoginResponse> loginService)
        {
            _loginService = loginService;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Dto.Login.LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var loginResponse = await _loginService.Login("Auth/LoginAdmin", loginRequest);
            if (loginResponse != null && loginResponse.Token !=null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = loginResponse.Token.Expiration
                };

                Response.Cookies.Append("AccessToken", loginResponse.Token.AccessToken, cookieOptions);
                Response.Cookies.Append("RefreshToken", loginResponse.Token.RefreshToken, cookieOptions);
                return RedirectToAction("Index", "AdminDashboard", new { area = "Admin" });
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginRequest);
        }

    }
}

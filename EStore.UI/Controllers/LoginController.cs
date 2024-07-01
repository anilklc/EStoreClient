using EStore.Dto.Login;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace EStore.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService<LoginResponse> _loginService;

        public LoginController(ILoginService<LoginResponse> loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(loginRequest);
            }

            var loginResponse = await _loginService.Login("Auth/Login", loginRequest);
            if (loginResponse != null && loginResponse.Token != null)
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = loginResponse.Token.Expiration
                };

                Response.Cookies.Append("AccessToken", loginResponse.Token.AccessToken, cookieOptions);
                Response.Cookies.Append("RefreshToken", loginResponse.Token.RefreshToken, cookieOptions);
                return RedirectToAction("Index", "Default");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(loginRequest);
        }
    }
}

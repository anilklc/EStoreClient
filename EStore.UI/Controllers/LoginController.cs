using EStore.Dto.Login;
using EStore.Dto.User;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace EStore.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService<LoginResponse> _loginService;
        private readonly IWriteService<object, UpdateForgotPassword> _writeService;

        public LoginController(ILoginService<LoginResponse> loginService, IWriteService<object, UpdateForgotPassword> writeService)
        {
            _loginService = loginService;
            _writeService = writeService;
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

        [HttpGet]
        public IActionResult ForgotPassword()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(Dto.User.ForgotPassword forgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _writeService.AddAsync(forgotPassword, "Auth/PasswordReset");
            return RedirectToAction("Index", "Default");
        }

        [HttpGet("[action]/{id}/{resetToken}")]
        public IActionResult UpdatePassword(string id,string resetToken)
        {
             return View();
        }

        [HttpPost("[action]/{id}/{resetToken}")]
        public async Task<IActionResult> UpdatePassword(UpdateForgotPassword updateForgotPassword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _writeService.AddAsync(updateForgotPassword, "Auth/ForgotPassword");
            return RedirectToAction("Index", "Default");
        }
    }
}

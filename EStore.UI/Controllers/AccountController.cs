using EStore.Dto.Login;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace EStore.UI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILoginService<Logout> _loginService;

        public AccountController(ILoginService<Logout> loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("/Account/Logout")]
        public async Task<IActionResult> Logout()
        {
            Logout logout = new() { userName = UserHelper.GetUserName(HttpContext)};
            await _loginService.Logout("Auth/Logout", logout);
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");
            return RedirectToAction("Index", "Default");

        }
    }
}

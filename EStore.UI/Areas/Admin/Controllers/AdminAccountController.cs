using EStore.Dto.Login;
using EStore.Services.Helper;
using EStore.Services.Interfaces;
using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminAccount")]
    public class AdminAccountController : BaseController
    {
        private readonly ILoginService<Logout> _loginService;

        public AdminAccountController(ILoginService<Logout> loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            Logout logout = new() { userName = UserHelper.GetUserName(HttpContext) };
            await _loginService.Logout("Auth/Logout", logout);
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");
            return RedirectToAction("Index", "AdminLogin", new { area = "Admin" });

        }
    }
}

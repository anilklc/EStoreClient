using EStore.Dto.Login;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        [Route("/Account/Logout")]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");
            return RedirectToAction("Index", "Default");

        }
    }
}

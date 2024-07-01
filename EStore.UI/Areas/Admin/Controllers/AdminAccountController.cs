using EStore.UI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminAccount")]
    public class AdminAccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");
            return RedirectToAction("Index", "AdminLogin", new { area = "Admin" });

        }
    }
}

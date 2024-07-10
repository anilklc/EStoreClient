using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserAddress")]
    public class UserAddressController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

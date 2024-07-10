using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Account.Controllers
{
    [Area("Account")]
    [Route("Account/UserOrder")]
    public class UserOrderController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

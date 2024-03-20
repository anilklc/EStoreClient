using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

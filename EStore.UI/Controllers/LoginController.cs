using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

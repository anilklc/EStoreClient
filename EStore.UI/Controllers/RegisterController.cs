using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

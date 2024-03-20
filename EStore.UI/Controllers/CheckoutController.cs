using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

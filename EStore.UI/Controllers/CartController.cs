using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

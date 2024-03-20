using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

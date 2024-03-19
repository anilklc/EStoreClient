using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

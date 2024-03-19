using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

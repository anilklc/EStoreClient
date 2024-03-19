using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

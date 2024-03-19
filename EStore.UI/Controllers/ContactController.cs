using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

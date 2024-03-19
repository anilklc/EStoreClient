using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class TermsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

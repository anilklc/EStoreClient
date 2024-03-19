using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    public class PoliciesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

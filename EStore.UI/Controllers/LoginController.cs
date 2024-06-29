using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminAbout")]
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}

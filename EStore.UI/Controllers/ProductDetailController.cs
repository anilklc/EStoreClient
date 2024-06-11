using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EStore.UI.Controllers
{
    public class ProductDetailController : Controller
    {
        public IActionResult Index(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
    }
}

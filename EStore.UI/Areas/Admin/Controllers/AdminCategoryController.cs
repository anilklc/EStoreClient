using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    public class AdminCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

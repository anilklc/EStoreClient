using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EStore.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminDashboard")]
    [Authorize(Roles = "Admin,Editor")]
    public class AdminDashboardController : Controller
    {
        [Route("Index")]

        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EStore.UI.Controllers
{
    public class BaseController : Controller
    {
        protected async Task<IActionResult> HandleServiceAction(Func<Task> action, string successMessage, string errorMessage)
        {
            try
            {
                await action();
                TempData["AlertMessage"] = successMessage;
                TempData["AlertType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["AlertMessage"] = errorMessage;
                TempData["AlertType"] = "error";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

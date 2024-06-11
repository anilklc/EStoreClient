using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EStore.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(int page)
        {
            ViewBag.Page = page;
            return View();
        }
       
    }
}

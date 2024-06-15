using EStore.Dto.Product;
using EStore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EStore.UI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index(int page,string brandName,string categoryName,float? minPrice, float? maxPrice)
        {
            ViewBag.Page = page;
            ViewBag.BrandName = brandName;
            ViewBag.CategoryName = categoryName;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            return View();
        }

     

    }
}
